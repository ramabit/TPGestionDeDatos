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
    public partial class ListadoEditarRol : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public Object SelectedItem { get; set; }      

        public ListadoEditarRol()
        {
            InitializeComponent();
        }

        private void ListadoEditarRol_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Habilitado");
            comboBox1.Items.Add("Deshabilitado");
            AgregarBotonEditar();
            AgregarListenerBotonEditar();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CargarRoles(int estadoRol)
        {
            DataSet roles = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();            
            
            parametros.Clear();
            parametros.Add(new SqlParameter("@habilitado", estadoRol));

            command = builderDeComandos.Crear("SELECT distinct * FROM Rol WHERE habilitado = @habilitado", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(roles);
            dataGridView1.DataSource = roles.Tables[0].DefaultView;            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "Habilitado")
            {
                CargarRoles(1);
            }
            else
            {
                CargarRoles(0);
            }
            
        }

        private void AgregarBotonEditar()
        {
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Editar";
                buttons.Text = "Editar";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;                
            }

            dataGridView1.Columns.Add(buttons);


        }

        private void AgregarListenerBotonEditar()
        {
            dataGridView1.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            String nombreRolAEditar = dataGridView1.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
            this.Hide();
            new EditarRol(nombreRolAEditar).Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RolForm().Show();
            this.Close();
        }

    }
}
