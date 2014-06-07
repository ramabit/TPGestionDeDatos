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
        private int estabaDeshabilitado;

        public EditarRol(String rol)
        {
            InitializeComponent();
            rolElegido = rol;            
            
        }

        private void EditarRol_Load(object sender, EventArgs e)
        {
            CargarTodosLosDatos();
        }

        private void CargarTodosLosDatos()
        {
            this.label3.Text = rolElegido;
            CargarFuncionalidades();            
            if (estaHabilitado())
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                estabaDeshabilitado = 0;
            }
            else
            {
                checkBox1.Checked = false;
                estabaDeshabilitado = 1;
            }
        }              

        private void botonVolver_Click(object sender, EventArgs e)
        {
            new RolForm().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ListadoEditarRol().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            CargarFuncionalidades();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(estabaDeshabilitado == 1 && checkBox1.Checked)
            {
                HabilitarRol();
            }

            AgregarFuncionalidades();
            QuitarFuncionalidades();

            if (this.textBox1.Text != "")
            {
                RenombrarRol();
                MessageBox.Show("Se modifico correctamente el rol " + rolElegido);
                rolElegido = this.textBox1.Text;
            }
            else
            {
                MessageBox.Show("Se modifico correctamente el rol " + rolElegido);
            }

            CargarTodosLosDatos();
            textBox1.Clear();
            
        }

        private bool estaHabilitado()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@rol", rolElegido));

            String consulta = "SELECT count(distinct nombre) FROM Rol WHERE nombre = @rol and habilitado = 1";
            int estadoRol = (int)builderDeComandos.Crear(consulta, parametros).ExecuteScalar();

            if (estadoRol == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        
        private void RenombrarRol()
        {            
            String nuevoNombreRol = this.textBox1.Text;

            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre_viejo", rolElegido));
            parametros.Add(new SqlParameter("@nombre_nuevo", nuevoNombreRol));

            String sql = "UPDATE Rol SET nombre = @nombre_nuevo WHERE nombre = @nombre_viejo";
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
            
            MessageBox.Show("El rol " + rolElegido + " fue renombrado como " + nuevoNombreRol);            
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

        private void HabilitarRol()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", rolElegido));
                        
            String sql = "UPDATE Rol SET habilitado = 1 WHERE nombre = @nombre";

            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
        }
               
        
    }
}

