﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Cliente
{
    public partial class FiltroCliente : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

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
            query = "SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento";
            parametros.Clear();
            command = builderDeComandos.Crear(query, parametros);

            DataSet tiposDeDocumentos = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(tiposDeDocumentos);
            comboBox_TipoDeDoc.DataSource = tiposDeDocumentos.Tables[0].DefaultView;
            comboBox_TipoDeDoc.ValueMember = "nombre";
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Cliente.Columns["id"].Visible = false;
        }

        private void CargarClientes()
        {
            command = builderDeComandos.Crear("SELECT c.id, u.username Username, c.nombre Nombre, c.apellido Apellido, td.nombre 'Tipo de Documento', c.documento Documento, c.fecha_nacimiento 'Fecha de Nacimiento', c.mail Mail, c.telefono Telefono, d.calle Calle, d.numero Numero, d.piso Piso, d.depto Departamento, d.cod_postal 'Codigo postal', d.localidad Localidad FROM LOS_SUPER_AMIGOS.Cliente c, LOS_SUPER_AMIGOS.TipoDeDocumento td, LOS_SUPER_AMIGOS.Direccion d, LOS_SUPER_AMIGOS.Usuario u WHERE c.tipo_de_documento_id = td.id AND c.direccion_id = d.id AND c.usuario_id = u.id", parametros);

            DataSet clientes = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(clientes);
            dataGridView_Cliente.DataSource = clientes.Tables[0].DefaultView;
            if (dataGridView_Cliente.Columns.Contains("Modificar")) 
                dataGridView_Cliente.Columns.Remove("Modificar");
            AgregarColumnaDeModificacion();
            AgregarListenerBotonDeModificacion();
        }

        private void AgregarColumnaDeModificacion()
        {
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Modificar";
            botonColumnaModificar.Name = "Modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Cliente.Columns.Add(botonColumnaModificar);
        }

        private void AgregarListenerBotonDeModificacion()
        {
            // Add a CellClick handler to handle clicks in the button column.
            dataGridView_Cliente.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Cliente_CellClick);
        }

        private void dataGridView_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Cliente.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idClienteAModificar = dataGridView_Cliente.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarCliente(idClienteAModificar).ShowDialog();
                CargarClientes();
            }
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = "ISNULL(usuario_id, 0) != 0";

            if (textBox_Nombre.Text != "") filtro += " and " + "nombre like '" + textBox_Nombre.Text + "%'";
            if (textBox_Apellido.Text != "") filtro += " and " + "apellido like '" + textBox_Apellido.Text + "%'";
            if (textBox_Mail.Text != "") filtro += " and " + "mail like '" + textBox_Mail.Text + "%'";
            if (textBox_NumeroDeDoc.Text != "") filtro += " and " + "dni like '" + textBox_NumeroDeDoc.Text + "%'";
            // TODO: agregar el filtro del documento

            query = "SELECT * FROM LOS_SUPER_AMIGOS.Cliente WHERE " + filtro;

            command = builderDeComandos.Crear(query, parametros);

            DataSet clientes = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(clientes);
            dataGridView_Cliente.DataSource = clientes.Tables[0].DefaultView;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Nombre.Text = "";
            textBox_Apellido.Text = "";
            textBox_Mail.Text = "";
            textBox_NumeroDeDoc.Text = "";
            // TODO: agregar la limpieza del comboBox

            CargarClientes();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

    }
}
