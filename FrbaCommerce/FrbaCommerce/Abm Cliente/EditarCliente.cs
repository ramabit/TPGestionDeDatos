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
    public partial class EditarCliente : Form
    {
        private Decimal idCliente;
        private Decimal idDireccion;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public EditarCliente(String idCliente)
        {
            InitializeComponent();
            this.idCliente = Convert.ToDecimal(idCliente);
        }

        private void EditarCliente_Load(object sender, EventArgs e)
        {
            CargarTipoDeDocumentos(); 
            CargarDatos();
        }

        private void CargarTipoDeDocumentos()
        {
            comboBox_TipoDeDocumento.DataSource = comunicador.SelectDataTable("nombre", "TipoDeDocumento");
            comboBox_TipoDeDocumento.ValueMember = "nombre";
        }

        private void CargarDatos()
        {
            Cliente cliente = comunicador.ObtenerCliente(idCliente);

            this.idDireccion = cliente.GetIdDireccion();
            textBox_Nombre.Text = cliente.GetNombre();
            textBox_Apellido.Text = cliente.GetApellido();
            textBox_NumeroDeDoc.Text = cliente.GetNumeroDeDocumento();
            textBox_FechaDeNacimiento.Text = cliente.GetFechaDeNacimiento();
            textBox_Mail.Text = cliente.GetMail();
            textBox_Telefono.Text = cliente.GetTelefono();
            CargarDireccion(idDireccion);
            CargarTipoDeDocumento(cliente.GetIdTipoDeDocumento());
        }

        private void CargarTipoDeDocumento(Decimal idTipoDeDocumento)
        {
            TipoDeDocumento tipoDeDocumento = new TipoDeDocumento();
            tipoDeDocumento.SetId(idTipoDeDocumento);
            comboBox_TipoDeDocumento.SelectedValue = comunicador.ObtenerNombreDe(tipoDeDocumento);
        }

        private void CargarDireccion(Decimal idDireccion)
        {
            Direccion direccion = comunicador.ObtenerDireccion(idDireccion);
            textBox_Calle.Text = direccion.GetCalle();
            textBox_Numero.Text = direccion.GetNumero();
            textBox_Piso.Text = direccion.GetPiso();
            textBox_Departamento.Text = direccion.GetDepartamento();
            textBox_CodigoPostal.Text = direccion.GetCodigoPostal();
            textBox_Localidad.Text = direccion.GetLocalidad();
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            // Guarda en variables todos los campos de entrada
            String nombre = textBox_Nombre.Text;
            String apellido = textBox_Apellido.Text;
            String tipoDeDocumentoNombre = comboBox_TipoDeDocumento.Text;
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

            Boolean pudoModificar;

            // Averigua el id del tipo de documento a partir del nombre del tipo de documento
            TipoDeDocumento tipoDeDocumento = new TipoDeDocumento();
            tipoDeDocumento.SetNombre(tipoDeDocumentoNombre);
            Decimal idTipoDeDocumento = comunicador.ObtenerIdDe(tipoDeDocumento);

            // Update direccion
            try
            {
                Direccion direccion = new Direccion();
                direccion.SetCalle(calle);
                direccion.SetNumero(numero);
                direccion.SetPiso(piso);
                direccion.SetDepartamento(departamento);
                direccion.SetCodigoPostal(codigoPostal);
                direccion.SetLocalidad(localidad);
                pudoModificar = comunicador.ModificarDireccion(idDireccion, direccion);

                if (pudoModificar) MessageBox.Show("La direccion se modifico correctamente");
                else MessageBox.Show("La direccion no se pudo modificar correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos en direccion");
                return;
            }

            // Update cliente
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
                pudoModificar = comunicador.ModificarCliente(idCliente, cliente);
                if (pudoModificar) MessageBox.Show("El cliente se modifico correctamente");
                else MessageBox.Show("El cliente no se pudo modificar correctamente");
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

            this.Close();
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_FechaDeNacimiento_Click(object sender, EventArgs e)
        {
            this.monthCalendar_FechaDeNacimiento.Visible = true;
        }

        private void monthCalendar_FechaDeNacimiento_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            textBox_FechaDeNacimiento.Text = e.Start.ToShortDateString();
            monthCalendar_FechaDeNacimiento.Visible = false;
        }
    }
}
