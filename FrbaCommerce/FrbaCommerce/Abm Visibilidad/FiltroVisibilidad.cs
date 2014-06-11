using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Visibilidad
{
    public partial class FiltroVisibilidad : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public FiltroVisibilidad()
        {
            InitializeComponent();
        }

        private void FiltroVisibilidad_Load(object sender, EventArgs e)
        {
            CargarVisibilidad();
            OcultarColumnasQueNoDebenVerse();
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Visibilidad.Columns["id"].Visible = false;
        }

        private void CargarVisibilidad()
        {
            command = builderDeComandos.Crear("SELECT v.id, v.descripcion Descripcion, v.precio Precio, v.porcentaje Porcentaje, v.duracion Duracion FROM LOS_SUPER_AMIGOS.Visibilidad v", parametros);

            DataSet visibilidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(visibilidades);
            dataGridView_Visibilidad.DataSource = visibilidades.Tables[0].DefaultView;
            if (dataGridView_Visibilidad.Columns.Contains("Modificar"))
                dataGridView_Visibilidad.Columns.Remove("Modificar");
            AgregarColumnaDeModificacion();
            AgregarListenerBotonDeModificacion();
        }

        private void AgregarColumnaDeModificacion()
        {
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Modificar";
            botonColumnaModificar.Name = "Modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Visibilidad.Columns.Add(botonColumnaModificar);
        }

        private void AgregarListenerBotonDeModificacion()
        {
            // Add a CellClick handler to handle clicks in the button column.
            dataGridView_Visibilidad.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Visibilidad_CellClick);
        }

        private void dataGridView_Visibilidad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Visibilidad.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idVisibilidadAModificiar = dataGridView_Visibilidad.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarVisibilidad(idVisibilidadAModificiar).ShowDialog();
                CargarVisibilidad();
            }
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = "";

            if (textBox_Descripcion.Text != "") filtro += "descripcion like '" + textBox_Descripcion.Text + "%'";

            query = "SELECT * FROM LOS_SUPER_AMIGOS.Visibilidad WHERE " + filtro;

            command = builderDeComandos.Crear(query, parametros);

            DataSet visibilidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(visibilidades);
            dataGridView_Visibilidad.DataSource = visibilidades.Tables[0].DefaultView;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            CargarVisibilidad();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }
    }
}
