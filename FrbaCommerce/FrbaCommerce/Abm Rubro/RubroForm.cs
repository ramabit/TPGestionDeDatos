using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Rubro
{
    public partial class RubroForm : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public RubroForm()
        {
            InitializeComponent();
        }

        private void RubroForm_Load(object sender, EventArgs e)
        {
            CargarRubro();
            AgregarColumnaDeModificacion();
            AgregarListenerBotonDeModificacion();
        }

        private void CargarRubro()
        {
            command = builderDeComandos.Crear("SELECT * FROM Rubro", parametros);

            DataSet rubros = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(rubros);
            dataGridView_Rubro.DataSource = rubros.Tables[0].DefaultView;
        }

        private void AgregarColumnaDeModificacion()
        {
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "modificar";
            botonColumnaModificar.Name = "modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Rubro.Columns.Add(botonColumnaModificar);
        }

        private void AgregarListenerBotonDeModificacion()
        {
            // Add a CellClick handler to handle clicks in the button column.
            dataGridView_Rubro.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Rubro_CellClick);
        }

        private void dataGridView_Rubro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Rubro.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idRubroAModificiar = dataGridView_Rubro.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarRubro(idRubroAModificiar).ShowDialog();
            }
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = "";

            if (textBox_Descripcion.Text != "") filtro += "descripcion like '" + textBox_Descripcion.Text + "%'";

            query = "SELECT * FROM Rubro WHERE " + filtro;

            command = builderDeComandos.Crear(query, parametros);

            DataSet rubros = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(rubros);
            dataGridView_Rubro.DataSource = rubros.Tables[0].DefaultView;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            CargarRubro();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
