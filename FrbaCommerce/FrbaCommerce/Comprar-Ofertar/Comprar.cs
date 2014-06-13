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

        public Comprar(Decimal usuarioVendedor, int publicacion)
        {
            InitializeComponent();
            vendedorId = usuarioVendedor;
            publicacionId = publicacion;
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
                labelTelefono.Text = ((Decimal)readerCliente["telefono"]).ToString();
            }
            else
            {
                String queryEmpresa = "SELECT * FROM LOS_SUPER_AMIGOS.Empresa WHERE usuario_id = @usuario and habilitado = 1)";
                SqlDataReader readerEmpresa = builderDeComandos.Crear(queryEmpresa, parametros).ExecuteReader();
                readerEmpresa.Read();
                labelNombre.Text = (String)readerEmpresa["razon_social"];
                labelMail.Text = (String)readerEmpresa["mail"];
                labelTelefono.Text = ((Decimal)readerEmpresa["telefono"]).ToString();
            }
        }

        private void pedirDireccion()
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", vendedorId));

            String queryDireccion = "SELECT * FROM LOS_SUPER_AMIGOS.Direccion WHERE usuario_id = @usuario)";
            SqlDataReader readerDireccion = builderDeComandos.Crear(queryDireccion, parametros).ExecuteReader();
            readerDireccion.Read();

            labelCalle.Text = (String)readerDireccion["calle"] + " " + (Decimal)readerDireccion["numero"];
            labelDepartamento.Text = "Departamento " + (Decimal)readerDireccion["piso"] + "-" + (String)readerDireccion["dpto"];
            labelPostal.Text = ((Decimal)readerDireccion["cod_postal"]).ToString();
            labelLocalidad.Text = (String)readerDireccion["localidad"];
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

            MessageBox.Show("Contactese con el vendedor para finalizar la compra");
            this.Close();
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {            
            this.Close();
        }
    }
}
