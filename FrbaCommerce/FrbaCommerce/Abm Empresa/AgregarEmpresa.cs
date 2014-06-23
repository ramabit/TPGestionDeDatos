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

namespace FrbaCommerce.ABM_Empresa
{
    public partial class AgregarEmpresa : Form
    {
        private String username;
        private String contrasena;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();
        private Decimal idDireccion;
        private Decimal idUsuario;

        public AgregarEmpresa(String username, String contrasena)
        {
            InitializeComponent();
            this.username = username;
            this.contrasena = contrasena;
            this.idDireccion = 0;
            this.idUsuario = 0;
        }

        private void AgregarEmpresa_Load(object sender, EventArgs e)
        {

        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            // Guarda en variables todos los campos de entrada
            String razonSocial = textBox_RazonSocial.Text;
            String nombreDeContacto = textBox_NombreDeContacto.Text;
            String cuit = textBox_CUIT.Text;
            DateTime fechaDeCreacion;
            DateTime.TryParse(textBox_FechaDeCreacion.Text, out fechaDeCreacion);
            String mail = textBox_Mail.Text;
            String telefono = textBox_Telefono.Text;
            String ciudad = textBox_Ciudad.Text;
            String calle = textBox_Calle.Text;
            String numero = textBox_Numero.Text;
            String piso = textBox_Piso.Text;
            String departamento = textBox_Departamento.Text;
            String codigoPostal = textBox_CodigoPostal.Text;
            String localidad = textBox_Localidad.Text;

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
            catch (FormatoInvalidoException exception)
            {
                MessageBox.Show("Datos mal ingresados");
                return;
            }
            // Controla que no se haya creado ya la direccion
            if (this.idDireccion == 0)
            {
                this.idDireccion = comunicador.CrearDireccion(direccion);
            }

            // Si la empresa lo crea el admin, crea un nuevo usuario predeterminado. Si lo crea un nuevo registro de usuario, usa el que viene por parametro
            if (idUsuario == 0)
            {
                idUsuario = CrearUsuario();
                MessageBox.Show("Se creo el usuario correctamente");
            }

            // Crea empresa
            try
            {
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
                empresa.SetHabilitado(true);
                Decimal idEmpresa = comunicador.CrearEmpresa(empresa);
                if (idEmpresa > 0) MessageBox.Show("Se agrego la empresa correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos");
                return;
            }
            catch (FormatoInvalidoException exception)
            {
                MessageBox.Show("Datos mal ingresados");
                return;
            }
            catch (TelefonoYaExisteException exception)
            {
                MessageBox.Show("Telefono ya existe");
                return;
            }
            catch (CuitYaExisteException exception)
            {
                MessageBox.Show("Cuit ya existe");
                return;
            }
            catch (RazonSocialYaExisteException exception)
            {
                MessageBox.Show("RazonSocial ya existe");
                return;
            }
            catch (FechaPasadaException exception)
            {
                MessageBox.Show("Fecha no valida");
                return;
            }

            VolverAlMenuPrincipal();
        }

        private Decimal CrearUsuario()
        {
            if (username == "clienteCreadoPorAdmin")
            {
                return comunicador.CrearUsuario();
            }
            else
            {
                return comunicador.CrearUsuarioConValores(username, contrasena);
            }
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
