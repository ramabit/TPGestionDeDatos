using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FrbaCommerce.Comprar_Ofertar
{
    public partial class Comprar : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;
        private int vendedorId;
        private int publicacionId;

        public Comprar(int usuarioVendedor, int publicacion)
        {
            InitializeComponent();
            vendedorId = usuarioVendedor;
            publicacionId = publicacion;
        }

        private void Comprar_Load(object sender, EventArgs e)
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", vendedorId));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Usuario WHERE id = (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            
            vendedorId = (int)reader["id"];
        }

        private void buttonConfirmarCompra_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO LOS_SUPER_AMIGOS.Compra(cantidad, fecha, usuario_id, publicacion_id, calificacion_id) VALUES (@cant, @fecha, @usuario, @publicacion, NULL)";
            DateTime fecha = DateTime.Now;
            parametros.Clear();
            parametros.Add(new SqlParameter("@cant", this.textBoxCant.Text));
            parametros.Add(new SqlParameter("@fecha", fecha));
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));
            parametros.Add(new SqlParameter("@publicacion", publicacionId));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
        }
    }
}
