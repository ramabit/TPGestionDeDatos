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
    public partial class Preguntar : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;        
        private int publicacionId;

        public Preguntar(int publicacion)
        {
            InitializeComponent();
            publicacionId = publicacion;
        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonPreguntar_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO LOS_SUPER_AMIGOS.Pregunta(descripcion, respuesta, respuesta_fecha, usuario_id, publicacion_id) VALUES (@descripcion, '', NULL, @usuario, @publicacion)";
            DateTime fecha = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DateKey"]);
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", this.textBoxPregunta.Text));            
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));
            parametros.Add(new SqlParameter("@publicacion", publicacionId));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
            MessageBox.Show("Pregunta enviada");
            this.Close();
        }
    }
}
