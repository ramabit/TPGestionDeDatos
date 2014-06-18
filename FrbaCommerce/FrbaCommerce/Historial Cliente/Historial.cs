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
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

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

            if (opcion == "Compras") CargarInformacion("publicacion.descripcion Producto, compra.cantidad Cantidad, publicacion.precio Precio, compra.fecha Fecha, a_quien.username 'A quien'", "LOS_SUPER_AMIGOS.Compra compra, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario a_quien", "compra.publicacion_id = publicacion.id AND publicacion.usuario_id = a_quien.id AND compra.usuario_id = @idUsuario");
            if (opcion == "Ofertas") CargarInformacion("user1.username 'De', user2.username 'A quien', oferta.monto 'Monto ofertado', oferta.fecha 'Cuando oferto', publicacion.descripcion 'Publicacion', LOS_SUPER_AMIGOS.gano_subasta(oferta.id) 'Gano la subasta'", "LOS_SUPER_AMIGOS.Oferta oferta, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario user1, LOS_SUPER_AMIGOS.Usuario user2", "oferta.publicacion_id = publicacion.id AND oferta.usuario_id = user1.id AND publicacion.usuario_id = user2.id AND (oferta.usuario_id = @idUsuario OR publicacion.usuario_id = @idUsuario)");
            if (opcion == "Calificaciones") CargarInformacion("user1.username 'De', user2.username 'A quien', calificacion.cantidad_estrellas 'Estrellas', calificacion.descripcion 'Descripcion calificacion', publicacion.descripcion 'Publicacion', publicacion.tipo 'Tipo de publicacion', compra.fecha 'Cuando', compra.cantidad 'Cuantos productos', (compra.cantidad * publicacion.precio) 'Monto pagado'", "LOS_SUPER_AMIGOS.Compra compra, LOS_SUPER_AMIGOS.Calificacion calificacion, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario user1, LOS_SUPER_AMIGOS.Usuario user2", "compra.calificacion_id = calificacion.id AND compra.usuario_id = user1.id AND publicacion.usuario_id = user2.id AND compra.publicacion_id = publicacion.id AND (compra.usuario_id = @idUsuario OR publicacion.usuario_id = @idUsuario)");
        }

        public void CargarInformacion(String select, String from, String where)
        {
            dataGridView_Historial.DataSource = comunicador.SelectDataTableConUsuario(select, from, where);
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
