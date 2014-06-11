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

            query = "SELECT calle, numero, piso, depto, cod_postal, localidad FROM LOS_SUPER_AMIGOS.Direccion WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idDireccion", Convert.ToDecimal(reader["direccion_id"])));
            idDireccion = Convert.ToString(reader["direccion_id"]);
            SqlDataReader readerDireccion = builderDeComandos.Crear(query, parametros).ExecuteReader();

            // Si no encuentra la direccion, tira error
            if (!readerDireccion.Read()) throw new Exception("No existe la direccion");

            textBox_Calle.Text = Convert.ToString(readerDireccion["calle"]);
            textBox_Numero.Text = Convert.ToString(readerDireccion["numero"]);
            textBox_Piso.Text = Convert.ToString(readerDireccion["piso"]);
            textBox_Departamento.Text = Convert.ToString(readerDireccion["depto"]);
            textBox_CodigoPostal.Text = Convert.ToString(readerDireccion["cod_postal"]);
            textBox_Localidad.Text = Convert.ToString(readerDireccion["localidad"]);
            if (Convert.ToBoolean(reader["habilitado"])) checkBox_Habilitado.Checked = true;
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

            // Controla que esten los campos numeroDeDocumento y telefono
            if (!this.pasoControlDeNoVacio(razonSocial)) return;
            if (!this.pasoControlDeNoVacio(cuit)) return;

            // Controla que el cuit no se haya registrado en el sistema
            if (!this.pasoControlDeRegistroDeCuit(cuit)) return;

            // Controla que la razon social no se encuentren registrado en el sistema
            if (!this.pasoControlDeRegistroDeRazonSocial(razonSocial)) return;

            // Controla que telefono sea unico
            if (telefono != "" && !this.pasoControlDeUnicidad(telefono)) return;

            // Update direccion
            query = "UPDATE LOS_SUPER_AMIGOS.Direccion SET calle = @calle, numero = @numero, piso = @piso, depto = @departamento, cod_postal = @codigoPostal, localidad = @localidad WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@calle", calle));
            parametros.Add(new SqlParameter("@numero", this.siEstaVacioDevuelveDBNullSinoDecimal(numero)));
            parametros.Add(new SqlParameter("@piso", this.siEstaVacioDevuelveDBNullSinoDecimal(piso)));
            parametros.Add(new SqlParameter("@departamento", departamento));
            parametros.Add(new SqlParameter("@codigoPostal", codigoPostal));
            parametros.Add(new SqlParameter("@localidad", localidad));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("La direccion se modifico correctamente");

            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            parametros.Add(new SqlParameter("@nombreDeContacto", nombreDeContacto));
            parametros.Add(new SqlParameter("@cuit", this.siEstaVacioDevuelveDBNullSinoDecimal(cuit)));
            parametros.Add(new SqlParameter("@fechaDeCreacion", fechaDeCreacion));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@telefono", this.siEstaVacioDevuelveDBNullSinoDecimal(telefono)));
            parametros.Add(new SqlParameter("@ciudad", ciudad));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));

            query = "UPDATE LOS_SUPER_AMIGOS.Empresa SET razon_social = @razonSocial, nombre_de_contacto = @nombreDeContacto, cuit = @cuit, fecha_creacion = @fechaDeCreacion, mail = @mail, telefono = @telefono, ciudad = @ciudad  WHERE id = @idEmpresa";

            filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("El cliente se modifico correctamente");

            this.Close();
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

        private object siEstaVacioDevuelveDBNullSinoDecimal(string valor)
        {
            if (valor == "")
            {
                return DBNull.Value;
            }
            else
            {
                return Convert.ToDecimal(valor);
            }
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
    }
}
