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
using FrbaCommerce.Exceptions;

namespace FrbaCommerce.ABM_Cliente
{
    public partial class AgregarCliente : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private String username;
        private String contrasena;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public AgregarCliente(String username, String contrasena)
        {
            InitializeComponent();
            this.username = username;
            this.contrasena = contrasena;
        }

        private void AgregarCliente_Load(object sender, EventArgs e)
        {
            CargarTipoDeDocumentos();
            AgregarListenerACalendario();
        }

        private void AgregarListenerACalendario()
        {
            this.monthCalendar_FechaDeNacimiento.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_FechaDeNacimiento_DateSelected);
        }

        private void CargarTipoDeDocumentos()
        {
            command = builderDeComandos.Crear("SELECT nombre Nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento", parametros);

            DataSet tiposDeDocumento = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(tiposDeDocumento);
            comboBox_TipoDeDocumento.DataSource = tiposDeDocumento.Tables[0].DefaultView;
            comboBox_TipoDeDocumento.ValueMember = "Nombre";
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            // Guarda en variables todos los campos de entrada
            String nombre = textBox_Nombre.Text;
            String apellido = textBox_Apellido.Text;
            String tipoDeDocumento = comboBox_TipoDeDocumento.Text;
            String numeroDeDocumento = textBox_NumeroDeDoc.Text;
            String fechaDeNacimiento = textBox_FechaDeNacimiento.Text;
            String mail = textBox_Mail.Text;
            String telefono = textBox_Telefono.Text;
            String calle = textBox_Calle.Text;
            String numero = textBox_Numero.Text;
            String piso = textBox_Piso.Text;
            String departamento = textBox_Departamento.Text;
            String codigoPostal = textBox_CodigoPostal.Text;
            String localidad = textBox_Localidad.Text;

            // Averigua el id del tipo de documento a partir del nombre del tipo de documento
            TipoDeDocumento tipoDeDocumentoObjeto = new TipoDeDocumento();
            tipoDeDocumentoObjeto.SetNombre(tipoDeDocumento);
            Decimal idTipoDeDocumento = comunicador.ObtenerIdDe(tipoDeDocumentoObjeto);

            // Crea una direccion y se guarda su id
            Direccion direccion = new Direccion();
            try
            {
                direccion.SetCalle(calle);
                direccion.SetNumero(numero);
                direccion.SetPiso(piso);
                direccion.SetDepartamento(departamento);
                direccion.SetCodigoPostal(codigoPostal);
                direccion.SetLocalidad(localidad);
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos en direccion");
                return;
            }
            Decimal idDireccion = comunicador.CrearDireccion(direccion);

            // Si el cliente lo crea el admin, crea un nuevo usuario predeterminado. Si lo crea un nuevo registro de usuario, usa el que viene por parametro
            Decimal idUsuario;
            if (username == "clienteCreadoPorAdmin")
            {
                idUsuario = comunicador.CrearUsuario();
            }
            else
            {
                idUsuario = comunicador.CrearUsuarioConValores(username, contrasena);
            }

            // Crear cliente
            try
            {
                Cliente cliente = new Cliente();
                cliente.SetNombre(nombre);
                cliente.SetApellido(apellido);
                cliente.SetFechaDeNacimiento(fechaDeNacimiento);
                cliente.SetMail(mail);
                cliente.SetTelefono(telefono);
                cliente.SetIdTipoDeDocumento(idTipoDeDocumento);
                cliente.SetNumeroDeDocumento(numeroDeDocumento);
                cliente.SetIdDireccion(idDireccion);
                cliente.SetIdUsuario(idUsuario);
                Decimal idCliente = comunicador.CrearCliente(cliente);
                if (idCliente > 0) MessageBox.Show("Se agrego el cliente correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos");
                return;
            }
            catch (ClienteYaExisteException exception)
            {
                MessageBox.Show("El documento ya existe");
                return;
            }
            catch (TelefonoYaExisteException exception)
            {
                MessageBox.Show("El telefono ya existe");
                return;
            }
            
            VolverAlMenuPrincial();
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Nombre.Text = "";
            textBox_Apellido.Text = "";
            comboBox_TipoDeDocumento.SelectedIndex = 0;
            textBox_NumeroDeDoc.Text = "";
            textBox_FechaDeNacimiento.Text = "";
            textBox_Mail.Text = "";
            textBox_Telefono.Text = "";
            textBox_Calle.Text = "";
            textBox_Numero.Text = "";
            textBox_Piso.Text = "";
            textBox_Departamento.Text = "";
            textBox_CodigoPostal.Text = "";
            textBox_Localidad.Text = "";
        }      

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            VolverAlMenuPrincial();
        }

        private void VolverAlMenuPrincial()
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void button_FechaDeNacimiento_Click(object sender, EventArgs e)
        {
            monthCalendar_FechaDeNacimiento.Visible = true;
        }

        private void monthCalendar_FechaDeNacimiento_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            textBox_FechaDeNacimiento.Text = e.Start.ToShortDateString();
            monthCalendar_FechaDeNacimiento.Visible = false;
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

        private bool pasoControlDeRegistro(Decimal tipoDeDocumento, String numeroDeDocumento)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente WHERE tipo_de_documento_id = @tipoDeDocumento AND documento = @numeroDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", Convert.ToDecimal(numeroDeDocumento)));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
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
    }
}
