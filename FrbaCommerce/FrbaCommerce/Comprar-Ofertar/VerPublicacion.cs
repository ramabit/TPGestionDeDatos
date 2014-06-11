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
        private int publicacionId;
        private int vendedorId;

        public VerPublicacion(String publicacion)
        {
            InitializeComponent();
            publicacionElegida = publicacion;
            tipoPublicacion = pedirEstado();
        }

        private void ComprarOfertar_Load(object sender, EventArgs e)
        {
            pedirVendedor();
            pedirRubro();
            pedirVencimientoStockPreguntas();
            labelProductoDatos.Text = publicacionElegida;            
            pedirPrecio();            
            pedirAccion();            
        }

        private String pedirEstado()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            return (String)reader["estado"];
        }

        private void pedirVendedor()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));            

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Usuario WHERE id = (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            labelVendedorDatos.Text = (String)reader["username"];
            vendedorId = (int)reader["id"];
        }

        private void pedirRubro()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Rubro WHERE id = (SELECT rubro_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            labelRubroDatos.Text = (String)reader["descripcion"];
        }

        private void pedirVencimientoStockPreguntas()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            labelVencimientoDatos.Text = ( (DateTime)reader["fecha_vencimiento"] ).ToString();
            labelStockDatos.Text = ( (int)reader["stock"] ).ToString();
            if ((int)reader["se_realizan_preguntas"] == 0)
            {
                botonPreguntar.Enabled = false;
            }
            publicacionId = (int)reader["publicacion_id"];
        }        

        private void pedirPrecio()
        {
            if (tipoPublicacion == "Compra Inmediata")
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

                String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion";

                SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
                labelPrecioDatos.Text = ( (int)reader["precio"] ).ToString();
            }
            else
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@descripcion", publicacionElegida));

                String query = "SELECT * FROM VistaOfertaMax WHERE publicacion_id = (SELECT publicacion_id FROM LOS_SUPER_AMIGOS.Publicacion WHERE descripcion = @descripcion)";

                SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
                labelPrecioDatos.Text = ( (int)reader["precioMax"] ).ToString();
            }
        }

        private void pedirAccion()
        {
            if (tipoPublicacion == "Compra Inmediata")
            {
                botonComprarOfertar.Text = "Comprar";
            }
            else
            {
                botonComprarOfertar.Text = "Ofertar";
            }
        }               

        private void botonComprarOfertar_Click(object sender, EventArgs e)
        {
            if (tipoPublicacion == "Compra Inmediata")
            {                          
                new Comprar(vendedorId, publicacionId).Show();                
            }
            else
            {
                new Ofertar(Convert.ToInt32(labelPrecioDatos.Text),publicacionId).Show();    
            }
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {            
            new BuscadorPublicaciones().Show();
            this.Close();
        }

        private void botonPreguntar_Click(object sender, EventArgs e)
        {
            new Preguntar().Show();
        }
    }
}
