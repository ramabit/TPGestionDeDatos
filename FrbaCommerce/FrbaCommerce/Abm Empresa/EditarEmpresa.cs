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
    public partial class EditarEmpresa : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private Decimal idEmpresa;
        private Decimal idDireccion;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public EditarEmpresa(String idEmpresa)
        {
            InitializeComponent();
            this.idEmpresa = Convert.ToDecimal(idEmpresa);
        }

        private void EditarEmpresa_Load(object sender, EventArgs e)
        {
            CargarDatos();
            AgregarListenerACalendario();
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Empresa WHERE id = @idEmpresa";

            parametros.Clear();
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();

            // Si no se puede leer tiro una excepcion
            if (!reader.Read()) throw new Exception("No se puede leer empresa");

            // Si se puede leer cargo los datos
            textBox_RazonSocial.Text = Convert.ToString(reader["razon_social"]);
            textBox_NombreDeContacto.Text = Convert.ToString(reader["nombre_de_contacto"]);
            textBox_CUIT.Text = Convert.ToString(reader["cuit"]);
            textBox_FechaDeCreacion.Text = Convert.ToString(reader["fecha_creacion"]);
            textBox_Mail.Text = Convert.ToString(reader["mail"]);
            textBox_Telefono.Text = Convert.ToString(reader["telefono"]);
            textBox_Ciudad.Text = Convert.ToString(reader["ciudad"]);
            idDireccion = Convert.ToDecimal(reader["direccion_id"]);

            CargarDireccion(idDireccion);

            if (Convert.ToBoolean(reader["habilitado"])) checkBox_Habilitado.Checked = true;
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

            Boolean pudoModificar;

            // Update direccion
            Direccion direccion = new Direccion();
            try
            {
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

            // Update empresa
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

                pudoModificar = comunicador.ModificarEmpresa(idEmpresa, empresa);
                if (pudoModificar) MessageBox.Show("La empresa se modifico correctamente");
                else MessageBox.Show("La empresa no se pudo modificar correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos");
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

        private void button_FechaDeCreacion_Click(object sender, EventArgs e)
        {
            monthCalendar_FechaDeCreacion.Visible = true;
        }

        private void AgregarListenerACalendario()
        {
            this.monthCalendar_FechaDeCreacion.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_FechaDeCreacion_DateSelected);
        }

        private void monthCalendar_FechaDeCreacion_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            textBox_FechaDeCreacion.Text = e.Start.ToShortDateString();
            monthCalendar_FechaDeCreacion.Visible = false;
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
    }
}
