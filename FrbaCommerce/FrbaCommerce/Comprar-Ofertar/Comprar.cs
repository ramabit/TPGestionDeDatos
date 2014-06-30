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
        private Decimal vendedorId;
        private int publicacionId;
        private int stockActual;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public Comprar(Decimal usuarioVendedor, int publicacion, int stock)
        {
            InitializeComponent();
            vendedorId = usuarioVendedor;
            publicacionId = publicacion;
            stockActual = stock;
        }

        private void Comprar_Load(object sender, EventArgs e)
        {
            pedirContacto();
            pedirDireccion();            
        }

        private void pedirContacto()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", vendedorId));

            String queryCliente = "SELECT * FROM LOS_SUPER_AMIGOS.Cliente WHERE usuario_id = @usuario and habilitado = 1";
            SqlDataReader readerCliente = builderDeComandos.Crear(queryCliente, parametros).ExecuteReader();

            if (readerCliente.Read())
            {
                labelNombre.Text = (String)readerCliente["nombre"] + " " + (String)readerCliente["apellido"];
                labelMail.Text = (String)readerCliente["mail"];
                if ((Decimal)readerCliente["telefono"] == 0)
                {
                    labelLocalidad.Text = "";
                }
                else
                {
                    labelLocalidad.Text = ((Decimal)readerCliente["telefono"]).ToString();
                }
            }
            else
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@usuario", vendedorId));
                String queryEmpresa = "SELECT * FROM LOS_SUPER_AMIGOS.Empresa WHERE usuario_id = @usuario and habilitado = 1";
                SqlDataReader readerEmpresa = builderDeComandos.Crear(queryEmpresa, parametros).ExecuteReader();
                readerEmpresa.Read();
                labelNombre.Text = (String)readerEmpresa["razon_social"];
                labelMail.Text = (String)readerEmpresa["mail"];                
                if ((Decimal)readerEmpresa["telefono"] == 0)
                {
                    labelLocalidad.Text = "";
                }
                else
                {
                    labelLocalidad.Text = ((Decimal)readerEmpresa["telefono"]).ToString();
                }
            }
        }

        private void pedirDireccion()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", vendedorId));

            String queryDireccion = "SELECT * FROM LOS_SUPER_AMIGOS.Direccion WHERE id = @usuario";
            SqlDataReader readerDireccion = builderDeComandos.Crear(queryDireccion, parametros).ExecuteReader();
            readerDireccion.Read();

            labelCalle.Text = (String)readerDireccion["calle"] + " " + (Decimal)readerDireccion["numero"];
            labelDepartamento.Text = "Departamento " + (Decimal)readerDireccion["piso"] + "-" + (String)readerDireccion["depto"];
            labelPostal.Text = ((String)readerDireccion["cod_postal"]).ToString();
            if ((String)readerDireccion["localidad"] == "localidadMigrada")
            {
                labelLocalidad.Text = "";
            }
            else
            {
                labelLocalidad.Text = (String)readerDireccion["localidad"];
            }
        }

        private void buttonConfirmarCompra_Click(object sender, EventArgs e)
        {
            uint val = 0;
            if (!UInt32.TryParse(textBoxCant.Text, out val))
            {
                MessageBox.Show("Solo puede ingresar un número entero positivo");
                textBoxCant.Clear();
                return;
            }

            if (Convert.ToInt32(textBoxCant.Text) == 0)
            {
                MessageBox.Show("No puede hacer un pedido por 0 unidades");
                return;
            }

            if (Convert.ToInt32(textBoxCant.Text) > stockActual)
            {
                MessageBox.Show("Su pedido excede el stock actual de " + stockActual + " unidades");
                return;
            }

            String sql = "INSERT INTO LOS_SUPER_AMIGOS.Compra(cantidad, fecha, usuario_id, publicacion_id, calificacion_id, facturada) VALUES (@cant, @fecha, @usuario, @publicacion, NULL,0)";
            DateTime fecha = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DateKey"]);

            parametros.Clear();
            parametros.Add(new SqlParameter("@cant", this.textBoxCant.Text));
            parametros.Add(new SqlParameter("@fecha", fecha));
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));
            parametros.Add(new SqlParameter("@publicacion", publicacionId));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();

            MessageBox.Show("Contactese con el vendedor para finalizar la compra");

            if (pedirEstado())
            {
                this.Hide();
                new VerPublicacion(publicacionId).ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                new BuscadorPublicaciones().ShowDialog();
                this.Close();
            }
        }

        private bool pedirEstado()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", publicacionId));

            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE id = @id";
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            reader.Read();

            Decimal idEstado = (Decimal)reader["estado_id"];
            String estado = (String) comunicador.SelectFromWhere("descripcion", "Estado", "id", idEstado)
            if (estado == "Finalizada")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new VerPublicacion(publicacionId).ShowDialog();
            this.Close();
        }
    }
}
