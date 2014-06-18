using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaCommerce.Objetos;

namespace FrbaCommerce.ABM_Cliente
{
    public partial class FiltroCliente : Form
    {
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public FiltroCliente()
        {
            InitializeComponent();
        }

        private void FiltroCliente_Load(object sender, EventArgs e)
        {
            CargarTiposDeDocumento();
            CargarClientes();
            OcultarColumnasQueNoDebenVerse();
        }

        private void CargarTiposDeDocumento()
        {
            comboBox_TipoDeDoc.DataSource = comunicador.SelectDataTable("nombre", "LOS_SUPER_AMIGOS.TipoDeDocumento");
            comboBox_TipoDeDoc.ValueMember = "nombre";
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Cliente.Columns["id"].Visible = false;
        }

        private void CargarClientes()
        {
            dataGridView_Cliente.DataSource = comunicador.SelectClientesParaFiltro();
            CargarColumnaModificacion();
            CargarColumnaEliminar();
        }

        private void CargarColumnaModificacion()
        {
            if (dataGridView_Cliente.Columns.Contains("Modificar"))
                dataGridView_Cliente.Columns.Remove("Modificar");
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Modificar";
            botonColumnaModificar.Name = "Modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Cliente.Columns.Add(botonColumnaModificar);
        }

        private void CargarColumnaEliminar()
        {
            if (dataGridView_Cliente.Columns.Contains("Eliminar"))
                dataGridView_Cliente.Columns.Remove("Eliminar");
            DataGridViewButtonColumn botonColumnaEliminar = new DataGridViewButtonColumn();
            botonColumnaEliminar.Text = "Eliminar";
            botonColumnaEliminar.Name = "Eliminar";
            botonColumnaEliminar.UseColumnTextForButtonValue = true;
            dataGridView_Cliente.Columns.Add(botonColumnaEliminar);
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = CalcularFiltro();
            dataGridView_Cliente.DataSource = comunicador.SelectClientesParaFiltroConFiltro(filtro);
        }

        private String CalcularFiltro()
        {
            String filtro = "";
            if (textBox_Nombre.Text != "") filtro += "AND " + "c.nombre LIKE '" + textBox_Nombre.Text + "%'";
            if (textBox_Apellido.Text != "") filtro += "AND " + "c.apellido LIKE '" + textBox_Apellido.Text + "%'";
            if (textBox_Mail.Text != "") filtro += "AND " + "c.mail LIKE '" + textBox_Mail.Text + "%'";
            if (textBox_NumeroDeDoc.Text != "") filtro += "AND " + "c.documento LIKE '" + textBox_NumeroDeDoc.Text + "%'";
            Decimal idTipoDeDocumento = (Decimal)comunicador.SelectFromWhere("id", "TipoDeDocumento", "nombre", comboBox_TipoDeDoc.Text);
            filtro += "AND " + "c.tipo_de_documento_id = " + idTipoDeDocumento;
            return filtro;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Nombre.Text = "";
            textBox_Apellido.Text = "";
            textBox_Mail.Text = "";
            textBox_NumeroDeDoc.Text = "";
            comboBox_TipoDeDoc.SelectedIndex = 0;
            CargarClientes();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void dataGridView_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Cliente.Columns["Modificar"].Index && e.RowIndex >= 0)
            {
                String idClienteAModificar = dataGridView_Cliente.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarCliente(idClienteAModificar).ShowDialog();
                CargarClientes();
                return;
            }
            if (e.ColumnIndex == dataGridView_Cliente.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                String idClienteAEliminar = dataGridView_Cliente.Rows[e.RowIndex].Cells["id"].Value.ToString();
                Boolean resultado = comunicador.Eliminar(Convert.ToDecimal(idClienteAEliminar), "Cliente");
                if (resultado) MessageBox.Show("Se elimino correctamente");
                CargarClientes();
                return;
            }
        }
    }
}
