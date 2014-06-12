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

namespace FrbaCommerce.ABM_Empresa
{
    public partial class AgregarEmpresa : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private String username;
        private String contrasena;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public AgregarEmpresa(String username, String contrasena)
        {
            InitializeComponent();
            this.username = username;
            this.contrasena = contrasena;
        }

        private void AgregarEmpresa_Load(object sender, EventArgs e)
        {
            AgregarListenerACalendario();
        }

        private void AgregarListenerACalendario()
        {
            this.monthCalendar_FechaDeCreacion.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_FechaDeCreacion_DateSelected);
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            // Guarda en variables todos los campos de entrada
            String razonSocial = textBox_RazonSocial.Text;
            String nombreDeContacto = textBox_NombreDeContacto.Text;
            String cuit = textBox_CUIT.Text;
            String fechaDeCreacion = textBox_FechaDeCreacion.Text;
            String mail = textBox_Mail.Text;
            String telefono = textBox_Telefono.Text;
            String ciudad = textBox_Ciudad.Text;
            String calle = textBox_Calle.Text;
            String numero = textBox_Numero.Text;
            String piso = textBox_Piso.Text;
            String departamento = textBox_Departamento.Text;
            String codigoPostal = textBox_CodigoPostal.Text;
            String localidad = textBox_Localidad.Text;

            // Controla que esten completos los campos
            if (!this.pasoControlDeNoVacio(razonSocial)) return;
            if (!this.pasoControlDeNoVacio(nombreDeContacto)) return;
            if (!this.pasoControlDeNoVacio(cuit)) return;
            if (!this.pasoControlDeNoVacio(fechaDeCreacion)) return;
            if (!this.pasoControlDeNoVacio(mail)) return;
            if (!this.pasoControlDeNoVacio(telefono)) return;
            if (!this.pasoControlDeNoVacio(ciudad)) return;
            if (!this.pasoControlDeNoVacio(calle)) return;
            if (!this.pasoControlDeNoVacio(numero)) return;
            if (!this.pasoControlDeNoVacio(piso)) return;
            if (!this.pasoControlDeNoVacio(departamento)) return;
            if (!this.pasoControlDeNoVacio(codigoPostal)) return;
            if (!this.pasoControlDeNoVacio(localidad)) return;

            // Controla que el cuit no se haya registrado en el sistema
            if (!this.pasoControlDeRegistroDeCuit(cuit)) return;

            // Controla que la razon social no se encuentren registrado en el sistema
            if (!this.pasoControlDeRegistroDeRazonSocial(razonSocial)) return;

            // Controla que telefono sea unico
            if (!this.pasoControlDeUnicidad(telefono)) return;

            // Crea una direccion y se guarda su id
            Direccion direccion = new Direccion();
            direccion.SetCalle(calle);
            direccion.SetNumero(numero);
            direccion.SetPiso(piso);
            direccion.SetDepartamento(departamento);
            direccion.SetCodigoPostal(codigoPostal);
            direccion.SetLocalidad(localidad);
            Decimal idDireccion = comunicador.CrearDireccion(direccion);

            // Si la empresa lo crea el admin, crea un nuevo usuario predeterminado. Si lo crea un nuevo registro de usuario, usa el que viene por parametro
            Decimal idUsuario;
            if (username == "clienteCreadoPorAdmin")
            {
                idUsuario = comunicador.CrearUsuario();
            }
            else
            {
                idUsuario = comunicador.CrearUsuarioConValores(username, contrasena);
            }

            Empresa empresa = new Empresa();
            empresa.SetRazonSocial(razonSocial);
            empresa.SetNombreDeContacto(nombreDeContacto);
            empresa.SetCuit(cuit);
            empresa.SetFechaDeCreacion(fechaDeCreacion);
            empresa.SetMail(mail);
            empresa.SetTelefono(telefono);
            empresa.SetCiudad(ciudad);
            empresa.SetIdDireccion(idDireccion);
            empresa.SetIdUsuario(idUsuario);
            Decimal idEmpresa = comunicador.CrearEmpresa(empresa);

            if (idEmpresa > 0) MessageBox.Show("Se agrego la empresa correctamente");
            /*
            // Hace el INSERT en Empresa
            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            parametros.Add(new SqlParameter("@nombreDeContacto", nombreDeContacto));
            parametros.Add(new SqlParameter("@cuit", cuit));
            parametros.Add(new SqlParameter("@fechaDeCreacion", fechaDeCreacion));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@telefono", Convert.ToDecimal(telefono)));
            parametros.Add(new SqlParameter("@ciudad", ciudad));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            parametros.Add(new SqlParameter("@idUsuario", idUsuario));

            query = "INSERT INTO LOS_SUPER_AMIGOS.Empresa (razon_social, nombre_de_contacto, cuit, fecha_creacion, mail, telefono, ciudad, direccion_id, usuario_id) values (@razonSocial, @nombreDeContacto, @cuit, @fechaDeCreacion, @mail, @telefono, @ciudad, @idDireccion, @idUsuario)";

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego la empresa correctamente");
            */
            VolverAlMenuPrincipal();
        }

        private bool pasoControlDeUnicidad(string telefono)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente WHERE telefono = @telefono";
            parametros.Clear();
            parametros.Add(new SqlParameter("@telefono", telefono));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                MessageBox.Show("Ya existe ese telefono");
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistroDeRazonSocial(String razonSocial)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razonSocial";
            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                MessageBox.Show("Ya existe esa razon social");
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistroDeCuit(String cuit)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE cuit = @cuit";
            parametros.Clear();
            parametros.Add(new SqlParameter("@cuit", cuit));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                MessageBox.Show("Ya existe ese cuit");
                return false;
            }
            return true;
        }

        private bool pasoControlDeNoVacio(string valor)
        {
            if (valor == "")
            {
                MessageBox.Show("Faltan datos");
                return false;
            }
            return true;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_RazonSocial.Text = "";
            textBox_NombreDeContacto.Text = "";
            textBox_CUIT.Text = "";
            textBox_FechaDeCreacion.Text = "";
            textBox_Mail.Text = "";
            textBox_Telefono.Text = "";
            textBox_Ciudad.Text = "";
            textBox_Calle.Text = "";
            textBox_Numero.Text = "";
            textBox_Piso.Text = "";
            textBox_Departamento.Text = "";
            textBox_CodigoPostal.Text = "";
            textBox_Localidad.Text = "";
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            VolverAlMenuPrincipal();
        }

        private void VolverAlMenuPrincipal()
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void button_FechaDeCreacion_Click(object sender, EventArgs e)
        {
            monthCalendar_FechaDeCreacion.Visible = true;
        }

        private void monthCalendar_FechaDeCreacion_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            textBox_FechaDeCreacion.Text = e.Start.ToShortDateString();
            monthCalendar_FechaDeCreacion.Visible = false;
        }
    }
}
