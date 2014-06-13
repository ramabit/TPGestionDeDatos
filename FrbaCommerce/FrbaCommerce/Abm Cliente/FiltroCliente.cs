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
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
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
            comboBox_TipoDeDoc.DataSource = comunicador.SelectDataTable("nombre", "TipoDeDocumento");
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

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = "";

            if (textBox_Nombre.Text != "") filtro += "AND " + "c.nombre LIKE '" + textBox_Nombre.Text + "%'";
            if (textBox_Apellido.Text != "") filtro += "AND " + "c.apellido LIKE '" + textBox_Apellido.Text + "%'";
            if (textBox_Mail.Text != "") filtro += "AND " + "c.mail LIKE '" + textBox_Mail.Text + "%'";
            if (textBox_NumeroDeDoc.Text != "") filtro += "AND " + "c.documento LIKE '" + textBox_NumeroDeDoc.Text + "%'";
            TipoDeDocumento tipoDeDocumento = new TipoDeDocumento();
            tipoDeDocumento.SetNombre(comboBox_TipoDeDoc.Text);
            Decimal idTipoDeDocumento = comunicador.ObtenerIdDe(tipoDeDocumento);
            filtro += "AND " + "c.tipo_de_documento_id = " + idTipoDeDocumento;

            query = "SELECT c.id, u.username Username, c.nombre Nombre, c.apellido Apellido, td.nombre 'Tipo de Documento', c.documento Documento, c.fecha_nacimiento 'Fecha de Nacimiento', c.mail Mail, c.telefono Telefono, d.calle Calle, d.numero Numero, d.piso Piso, d.depto Departamento, d.cod_postal 'Codigo postal', d.localidad Localidad FROM LOS_SUPER_AMIGOS.Cliente c, LOS_SUPER_AMIGOS.TipoDeDocumento td, LOS_SUPER_AMIGOS.Direccion d, LOS_SUPER_AMIGOS.Usuario u WHERE c.tipo_de_documento_id = td.id AND c.direccion_id = d.id AND c.usuario_id = u.id " + filtro;

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
            comboBox_TipoDeDoc.SelectedIndex = 0;
            CargarClientes();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
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
    }
}
