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
    public partial class EditarRol : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public Object SelectedItem { get; set; }
        private String rolElegido;

        public EditarRol(String rol)
        {
            InitializeComponent();
            rolElegido = rol;            
        }

        private void EditarRol_Load(object sender, EventArgs e)
        {
            this.label3.Text = rolElegido;
            CargarFuncionalidades();
            MarcarLasFuncionalidadesQueTiene();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarcarLasFuncionalidadesQueTiene();
        }              

        private void botonVolver_Click(object sender, EventArgs e)
        {
            new BajaRol().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
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
            MarcarLasFuncionalidadesQueTiene();            
        }

        private void MarcarLasFuncionalidadesQueTiene()
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            List<int> funcionalidadesAMarcar = new List<int>();
            DesmarcarFuncionalidades();

            foreach (DataRowView funcionalidad in this.checkedListBox1.Items)
            {                
                parametros.Clear();
                parametros.Add(new SqlParameter("@rol", rolElegido));
                parametros.Add(new SqlParameter("@funcionalidad", funcionalidad.Row["nombre"] as String));

                if (verificarSiLaTiene(funcionalidad.Row["nombre"] as String))
                {
                    int i = checkedListBox1.Items.IndexOf(funcionalidad);
                    funcionalidadesAMarcar.Add(i);
                    
                }                
            }

            foreach (int index in funcionalidadesAMarcar)
            {
                checkedListBox1.SetItemChecked(index, true);
            }
        }

        private void DesmarcarFuncionalidades()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }    
        

        private void button3_Click(object sender, EventArgs e)
        {
            AgregarFuncionalidades();
            QuitarFuncionalidades();
            if (this.textBox1.Text != "")
            {
                RenombrarRol();
            }            
        }

        private void RenombrarRol()
        {            
            String nuevoNombreRol = this.textBox1.Text;

            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre_viejo", rolElegido));
            parametros.Add(new SqlParameter("@nombre_nuevo", nuevoNombreRol));

            String sql = "UPDATE Rol SET nombre = @nombre_nuevo WHERE nombre = @nombre_viejo";
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
            
            MessageBox.Show("El rol " + rolElegido + " fue renombrado como " + nuevoNombreRol);
            textBox1.Clear();
        }

        private void AgregarFuncionalidades()
        {            

            foreach (DataRowView funcionalidad in this.checkedListBox1.CheckedItems)
            {
                if (verificarSiLaTiene(funcionalidad.Row["nombre"] as String))
                {

                }
                else
                {
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@rol", rolElegido));
                    parametros.Add(new SqlParameter("@funcionalidad", funcionalidad.Row["nombre"] as String));

                    String sql1 = "INSERT INTO Funcionalidad_x_Rol(funcionalidad_id, rol_id) VALUES ((SELECT id FROM Funcionalidad WHERE nombre = @funcionalidad), (SELECT  id FROM Rol WHERE nombre = @rol))";

                    builderDeComandos.Crear(sql1, parametros).ExecuteNonQuery();
                }
            }
        }

        private bool verificarSiLaTiene(String funcionalidad)
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@rol", rolElegido));
            parametros.Add(new SqlParameter("@funcionalidad", funcionalidad));

            String consulta = "SELECT count(*) FROM Funcionalidad_x_Rol WHERE funcionalidad_id = (SELECT id FROM Funcionalidad WHERE nombre = @funcionalidad) and rol_id = (SELECT  id FROM Rol WHERE nombre = @rol)";
            int tieneLaFuncionalidad = (int)builderDeComandos.Crear(consulta, parametros).ExecuteScalar();

                if (tieneLaFuncionalidad == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        private void QuitarFuncionalidades()
        {
            
            
            foreach (DataRowView funcionalidad in this.checkedListBox1.Items)
            {
                int index = checkedListBox1.Items.IndexOf(funcionalidad);
                String estado = this.checkedListBox1.GetItemCheckState(index).ToString();

                if (estado == "Unchecked")
                {
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@rol", rolElegido));
                    parametros.Add(new SqlParameter("@funcionalidad", funcionalidad.Row["nombre"] as String));

                    String sql2 = "DELETE Funcionalidad_x_Rol WHERE funcionalidad_id = (SELECT id FROM Funcionalidad WHERE nombre = @funcionalidad) and rol_id = (SELECT  id FROM Rol WHERE nombre = @rol)";

                    builderDeComandos.Crear(sql2, parametros).ExecuteNonQuery();
                }
            }
        }

        
    }
}

