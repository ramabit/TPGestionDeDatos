using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Historial_Cliente
{
    public partial class Historial : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public Historial()
        {
            InitializeComponent();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            CargarOpciones();
            CargarDatos();
        }

        private void CargarOpciones()
        {
            DataTable opciones = new DataTable();
            opciones.Columns.Add("opciones");
            opciones.Rows.Add("Compras");
            opciones.Rows.Add("Ofertas");
            opciones.Rows.Add("Calificaciones");
            comboBox_opciones.DataSource = opciones;
            comboBox_opciones.ValueMember = "opciones";
        }

        private void CargarDatos()
        {
            String opcion = comboBox_opciones.Text;

            if (opcion == "Compras") CargarInformacion("SELECT publicacion.descripcion Producto, compra.cantidad Cantidad, publicacion.precio Precio, compra.fecha Fecha, a_quien.username 'A quien' FROM LOS_SUPER_AMIGOS.Compra compra, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario a_quien  WHERE compra.publicacion_id = publicacion.id AND publicacion.usuario_id = a_quien.id AND compra.usuario_id = @idUsuario");
            if (opcion == "Ofertas") CargarInformacion("SELECT publicacion.descripcion Producto, compra.cantidad Cantidad, publicacion.precio Precio, compra.fecha Fecha, a_quien.username 'A quien' FROM LOS_SUPER_AMIGOS.Compra compra, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario a_quien  WHERE compra.publicacion_id = publicacion.id AND publicacion.usuario_id = a_quien.id AND compra.usuario_id = @idUsuario");
            if (opcion == "Calificaciones") CargarInformacion("SELECT publicacion.descripcion Producto, oferta.monto Cantidad, publicacion.precio Precio, oferta.fecha Fecha, a_quien.username 'A quien', LOS_SUPER_AMIGOS.gano_subasta(oferta.id) 'Gano la subasta' FROM LOS_SUPER_AMIGOS.Oferta oferta, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario a_quien  WHERE oferta.publicacion_id = publicacion.id AND publicacion.usuario_id = a_quien.id AND oferta.usuario_id = @idUsuario");
        }

        public void CargarInformacion(String query)
        {
            command = builderDeComandos.Crear(query, parametros);
            command.Parameters.Add(new SqlParameter("@idUsuario", UsuarioSesion.Usuario.id));
            DataSet compras = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(compras);
            dataGridView_Historial.DataSource = compras.Tables[0].DefaultView;
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            comboBox_opciones.SelectedIndex = 0;
            CargarDatos();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }
    }
}
