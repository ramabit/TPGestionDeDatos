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

<<<<<<< HEAD
        private void AgregarRol_Load_1(object sender, EventArgs e)
        {
            CargarFuncionalidades();
        }   

        private void CargarFuncionalidades()
        {
            DataSet funcionalidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();
            command = builderDeComandos.Crear("SELECT distinct nombre FROM Funcionalidad", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(funcionalidades);
            checkedListBox1.DataSource = funcionalidades.Tables[0].DefaultView;
            checkedListBox1.ValueMember = "nombre";
        }

        private void button1_Click(object sender, EventArgs e)
=======
        private void botonVolver_Click(object sender, EventArgs e)
>>>>>>> 7151b4edd1c426416de21c3b3362f2a7e2507bc6
        {
            new RolForm().Show();
            this.Close();
        }

<<<<<<< HEAD
        private void button2_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO Rol(nombre, habilitado) VALUES (@rol, 1)";
            parametros.Clear();
            parametros.Add(new SqlParameter("@rol", this.textBox1.Text));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();

            foreach (DataRowView funcionalidad in this.checkedListBox1.CheckedItems)
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@rol", this.textBox1.Text));

                parametros.Add(new SqlParameter("@funcionalidad", funcionalidad.Row["nombre"] as String));
                                                
                String sql2 = "INSERT INTO Funcionalidad_x_Rol(funcionalidad_id, rol_id) VALUES ((SELECT id FROM Funcionalidad WHERE nombre = @funcionalidad), (SELECT  id FROM Rol WHERE nombre = @rol))";
                                
                builderDeComandos.Crear(sql2, parametros).ExecuteNonQuery();                                
            }
            MessageBox.Show("Se creo el rol " + this.textBox1.Text);
            BorrarDatosIngresados();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BorrarDatosIngresados();            
        }

        private void BorrarDatosIngresados()
        {
            textBox1.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
                     
=======
>>>>>>> 7151b4edd1c426416de21c3b3362f2a7e2507bc6
    }
}
