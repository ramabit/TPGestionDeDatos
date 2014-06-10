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
    public partial class VerPublicacion : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public Object SelectedItem { get; set; }
        private String publicacionElegida;
        private String tipoPublicacion;

        public VerPublicacion(String publicacion)
        {
            InitializeComponent();
            publicacionElegida = publicacion;
            tipoPublicacion = pedirEstado();
        }

        private void ComprarOfertar_Load(object sender, EventArgs e)
        {
            labelVendedorDatos.Text = pedirVendedor();
            labelRubroDatos.Text = pedirRubro();
            labelVencimientoDatos.Text = pedirVencimiento().ToString();
            labelProductoDatos.Text = publicacionElegida;
            labelStockDatos.Text = pedirStock().ToString();
            labelPrecioDatos.Text = pedirPrecio().ToString();
            botonComprarOfertar.Text = pedirAccion();
            permitirPreguntas();
        }

        private String pedirEstado()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            return (String)reader["estado"];
        }

        private String pedirVendedor()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));            

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Usuario WHERE id = (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            return (String)reader["username"];
        }

        private String pedirRubro()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Rubro WHERE id = (SELECT rubro_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            return (String)reader["descripcion"];
        }

        private DateTime pedirVencimiento()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            return (DateTime)reader["fecha_vencimiento"];
        }

        private int pedirStock()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            return (int)reader["stock"];
        }

        private int pedirPrecio()
        {
            if (tipoPublicacion == "Compra Inmediata")
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

                String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

                SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
                return (int)reader["precio"];
            }
            else
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

                String query = "SELECT * FROM VistaOfertaMax WHERE publicacion_id = (SELECT publicacion_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

                SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
                return (int)reader["precioMax"];
            }
        }

        private String pedirAccion()
        {
            if (tipoPublicacion == "Compra Inmediata")
            {                
                return "Comprar";
            }
            else
            {
                return "Ofertar";
            }
        }

        private void permitirPreguntas()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if ((int)reader["se_realizan_preguntas"] == 0)
            {
                botonPreguntar.Enabled = false;
            }
        }

        private void botonComprarOfertar_Click(object sender, EventArgs e)
        {
            if (tipoPublicacion == "Compra Inmediata")
            {

                
            }
            else
            {
                
            }
        }
    }
}
