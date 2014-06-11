using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Empresa
{
    public partial class EditarEmpresa : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private String idEmpresa;
        private String idDireccion;

        public EditarEmpresa(String idEmpresa)
        {
            InitializeComponent();
            this.idEmpresa = idEmpresa;
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
            idDireccion = Convert.ToString(reader["direccion_id"]);

            CargarDireccion(idDireccion);

            if (Convert.ToBoolean(reader["habilitado"])) checkBox_Habilitado.Checked = true;
        }

        private void CargarDireccion(String idDireccion)
        {
            Direccion direccion = new Direccion();
            Boolean pudoObtenerDireccion = direccion.ObtenerDireccion(Convert.ToDecimal(idDireccion));

            if (!pudoObtenerDireccion) throw new Exception("No existe la direccion");

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

            // Update direccion
            Direccion direccion = new Direccion();
            direccion.SetCalle(calle);
            direccion.SetNumero(numero);
            direccion.SetPiso(piso);
            direccion.SetDepartamento(departamento);
            direccion.SetCodigoPostal(codigoPostal);
            direccion.SetLocalidad(localidad);
            Boolean pudoModificar = direccion.ModificarDireccion(Convert.ToDecimal(idDireccion));

            if (!pudoModificar) MessageBox.Show("La direccion se modifico correctamente");
            else MessageBox.Show("La direccion no se pudo modificar correctamente");

            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            parametros.Add(new SqlParameter("@nombreDeContacto", nombreDeContacto));
            parametros.Add(new SqlParameter("@cuit", Convert.ToDecimal(cuit)));
            parametros.Add(new SqlParameter("@fechaDeCreacion", fechaDeCreacion));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@telefono", Convert.ToDecimal(telefono)));
            parametros.Add(new SqlParameter("@ciudad", ciudad));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));

            query = "UPDATE LOS_SUPER_AMIGOS.Empresa SET razon_social = @razonSocial, nombre_de_contacto = @nombreDeContacto, cuit = @cuit, fecha_creacion = @fechaDeCreacion, mail = @mail, telefono = @telefono, ciudad = @ciudad  WHERE id = @idEmpresa";

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("El cliente se modifico correctamente");

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
