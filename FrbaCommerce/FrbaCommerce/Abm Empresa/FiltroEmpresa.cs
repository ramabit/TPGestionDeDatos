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
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public FiltroEmpresa()
        {
            InitializeComponent();
        }

        private void FiltroEmpresa_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
            OcultarColumnasQueNoDebenVerse();
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Empresa.Columns["id"].Visible = false;
        }

        private void CargarEmpresas()
        {
            dataGridView_Empresa.DataSource = comunicador.SelectEmpresasParaFiltro();
            CargarColumnaModificacion();
            CargarColumnaEliminar();
        }

        private void CargarColumnaModificacion()
        {
            if (dataGridView_Empresa.Columns.Contains("Modificar"))
                dataGridView_Empresa.Columns.Remove("Modificar");
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Modificar";
            botonColumnaModificar.Name = "Modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Empresa.Columns.Add(botonColumnaModificar);
            dataGridView_Empresa.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Empresa_CellClick);
        }

        private void CargarColumnaEliminar()
        {
            if (dataGridView_Empresa.Columns.Contains("Eliminar"))
                dataGridView_Empresa.Columns.Remove("Eliminar");
            DataGridViewButtonColumn botonColumnaEliminar = new DataGridViewButtonColumn();
            botonColumnaEliminar.Text = "Eliminar";
            botonColumnaEliminar.Name = "Eliminar";
            botonColumnaEliminar.UseColumnTextForButtonValue = true;
            dataGridView_Empresa.Columns.Add(botonColumnaEliminar);
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = CalcularFiltro();
            dataGridView_Empresa.DataSource = comunicador.SelectEmpresasParaFiltroConFiltro(filtro);
        }

        private String CalcularFiltro()
        {
            String filtro = "";
            if (textBox_RazonSocial.Text != "") filtro += "AND " + "e.razon_social LIKE '" + textBox_RazonSocial.Text + "%'";
            if (textBox_Cuit.Text != "") filtro += "AND " + "e.cuit LIKE '" + textBox_Cuit.Text + "%'";
            if (textBox_Mail.Text != "") filtro += "AND " + "e.mail LIKE '" + textBox_Mail.Text + "%'";
            return filtro;
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
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void dataGridView_Empresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Empresa.Columns["Modificar"].Index && e.RowIndex >= 0)
            {
                String idEmpresaAModificar = dataGridView_Empresa.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarEmpresa(idEmpresaAModificar).ShowDialog();
                CargarEmpresas();
                return;
            }
            if (e.ColumnIndex == dataGridView_Empresa.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                String idEmpresaAEliminar = dataGridView_Empresa.Rows[e.RowIndex].Cells["id"].Value.ToString();
                Boolean resultado = comunicador.Eliminar(Convert.ToDecimal(idEmpresaAEliminar), "Empresa");
                if (resultado) MessageBox.Show("Se elimino correctamente");
                CargarEmpresas();
                return;
            }
        }
    }
}
