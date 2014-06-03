using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Empresa
{
    public partial class FiltroEmpresa : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public FiltroEmpresa()
        {
            InitializeComponent();
        }

        private void FiltroEmpresa_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
            AgregarColumnaDeModificacion();
            AgregarListenerBotonDeModificacion();
        }

        private void CargarEmpresas()
        {
            command = builderDeComandos.Crear("SELECT * FROM Empresa", parametros);

            DataSet clientes = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(clientes);
            dataGridView_Empresa.DataSource = clientes.Tables[0].DefaultView;
        }

        private void AgregarColumnaDeModificacion()
        {
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "modificar";
            botonColumnaModificar.Name = "modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Empresa.Columns.Add(botonColumnaModificar);
        }

        private void AgregarListenerBotonDeModificacion()
        {
            // Add a CellClick handler to handle clicks in the button column.
            dataGridView_Empresa.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Empresa_CellClick);
        }

        private void dataGridView_Empresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Empresa.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idClienteAModificar = dataGridView_Empresa.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarEmpresa(idClienteAModificar).ShowDialog();
            }
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_RazonSocial.Text = "";
            textBox_Cuit.Text = "";
            textBox_Mail.Text = "";
            CargarEmpresas();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
