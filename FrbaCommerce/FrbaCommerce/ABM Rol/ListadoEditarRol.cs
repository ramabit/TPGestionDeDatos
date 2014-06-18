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
            comboBoxEstadoRoles.Items.Add("Habilitado");
            comboBoxEstadoRoles.Items.Add("Deshabilitado");         
        }

        private void comboBoxEstadoRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CargarRoles(int estadoRol)
        {
            DataSet roles = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();            
            
            parametros.Clear();
            parametros.Add(new SqlParameter("@habilitado", estadoRol));

            command = builderDeComandos.Crear("SELECT DISTINCT * FROM LOS_SUPER_AMIGOS.Rol WHERE habilitado = @habilitado", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(roles);
            dataGridViewResultadosBusqueda.DataSource = roles.Tables[0].DefaultView;
            AgregarBotonEditar();
        }

        private void botonBuscar_Click(object sender, EventArgs e)
        {
            if (this.comboBoxEstadoRoles.Text == "Habilitado")
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
            if (dataGridViewResultadosBusqueda.Columns.Contains("Editar"))
                dataGridViewResultadosBusqueda.Columns.Remove("Editar");
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Editar";
                buttons.Text = "Editar";
                buttons.Name = "Editar";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;                
            }

            dataGridViewResultadosBusqueda.Columns.Add(buttons);
            dataGridViewResultadosBusqueda.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridViewResultadosBusqueda.Columns["Editar"].Index)
            {
                String nombreRolAEditar = dataGridViewResultadosBusqueda.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
                this.Hide();
                new EditarRol(nombreRolAEditar).ShowDialog();
            }
            
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RolForm().ShowDialog();
            this.Close();
        }

    }
}
