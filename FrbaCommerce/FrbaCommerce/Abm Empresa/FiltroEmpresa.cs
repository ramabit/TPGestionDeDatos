﻿using System;
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
                String idEmpresaAModificar = dataGridView_Empresa.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarEmpresa(idEmpresaAModificar).ShowDialog();
            }
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = "ISNULL(usuario_id, 0) != 0";

            if (textBox_RazonSocial.Text != "") filtro += " and " + "razon_social like '" + textBox_RazonSocial.Text + "%'";
            if (textBox_Cuit.Text != "") filtro += " and " + "cuit like '" + textBox_Cuit.Text + "%'";
            if (textBox_Mail.Text != "") filtro += " and " + "mail like '" + textBox_Mail.Text + "%'";
            
            query = "SELECT * FROM Empresa WHERE " + filtro;

            command = builderDeComandos.Crear(query, parametros);

            DataSet empresas = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(empresas);
            dataGridView_Empresa.DataSource = empresas.Tables[0].DefaultView;
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
