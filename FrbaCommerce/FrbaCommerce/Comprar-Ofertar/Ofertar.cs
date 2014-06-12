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
    public partial class Ofertar : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;
        private int ofertaMax;
        private int publicacionId;

        public Ofertar(int montoMax,int publicacion)
        {
            InitializeComponent();
            publicacionId = publicacion;
            ofertaMax = montoMax;
        }

        private void Ofertar_Load(object sender, EventArgs e)
        {

        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonOfertar_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO LOS_SUPER_AMIGOS.Oferta(monto, gano_subasta, fecha, usuario_id, publicacion_id) VALUES (@monto, 0, @fecha, @usuario, @publicacion)";
            DateTime fecha = DateTime.Now;
            parametros.Clear();
            parametros.Add(new SqlParameter("@monto", this.textBoxMonto.Text));
            parametros.Add(new SqlParameter("@fecha", fecha));
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));
            parametros.Add(new SqlParameter("@publicacion", publicacionId));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
            MessageBox.Show("Su oferta fue registrada");
            this.Close();
        }
    }
}
