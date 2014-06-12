using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FrbaCommerce.ABM_Rol
{
    public partial class AgregarRol : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        
        public AgregarRol()
        {
            InitializeComponent();
        }

        private void AgregarRol_Load_1(object sender, EventArgs e)
        {
            CargarFuncionalidades();
        }   

        private void CargarFuncionalidades()
        {
            DataSet funcionalidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();
            command = builderDeComandos.Crear("SELECT DISTINCT nombre FROM LOS_SUPER_AMIGOS.Funcionalidad", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(funcionalidades);
            checkedListBoxFuncionalidades.DataSource = funcionalidades.Tables[0].DefaultView;
            checkedListBoxFuncionalidades.ValueMember = "nombre";
        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RolForm().Show();
            this.Close();
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO LOS_SUPER_AMIGOS.Rol(nombre, habilitado) VALUES (@rol, 1)";
            parametros.Clear();
            parametros.Add(new SqlParameter("@rol", this.textBoxRol.Text));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();

            foreach (DataRowView funcionalidad in this.checkedListBoxFuncionalidades.CheckedItems)
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@rol", this.textBoxRol.Text));

                parametros.Add(new SqlParameter("@funcionalidad", funcionalidad.Row["nombre"] as String));

                String sql2 = "INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad_x_Rol(funcionalidad_id, rol_id) VALUES ((SELECT id FROM LOS_SUPER_AMIGOS.Funcionalidad WHERE nombre = @funcionalidad), (SELECT  id FROM Rol WHERE nombre = @rol))";
                                
                builderDeComandos.Crear(sql2, parametros).ExecuteNonQuery();                                
            }
            MessageBox.Show("Se creo el rol " + this.textBoxRol.Text);
            BorrarDatosIngresados();
        }

        private void checkedListBoxFuncionalidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            BorrarDatosIngresados();            
        }

        private void BorrarDatosIngresados()
        {
            textBoxRol.Clear();
            for (int i = 0; i < checkedListBoxFuncionalidades.Items.Count; i++)
            {
                checkedListBoxFuncionalidades.SetItemChecked(i, false);
            }
        }

    }
}
