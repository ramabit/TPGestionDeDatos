using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            command = builderDeComandos.Crear("SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento", parametros);

            DataSet tiposDeDocumento = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(tiposDeDocumento);
            comboBox_TipoDeDocumento.DataSource = tiposDeDocumento.Tables[0].DefaultView;
            comboBox_TipoDeDocumento.ValueMember = "nombre";
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
            SqlParameter parametroOutput;

            // Controla que esten los campos numeroDeDocumento y telefono
            if (!this.pasoControlDeNoVacio(numeroDeDocumento)) return;
            if (!this.pasoControlDeNoVacio(telefono)) return;

            // Averigua el id del tipo de documento a partir del nombre del tipo de documento
            query = "SELECT id FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE nombre = @tipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            Decimal idTipoDeDocumento = (Decimal)builderDeComandos.Crear(query, parametros).ExecuteScalar();

            // Controla que tipo y numero de documento ya se haya registrado en el sistema
            if (!this.pasoControlDeRegistro(idTipoDeDocumento, numeroDeDocumento)) return;

            // Controla que telefono sea unico
            if (!this.pasoControlDeUnicidad(telefono)) return;

            // Crea una direccion y se guarda su id. Usa un stored procedure del script
            query = "LOS_SUPER_AMIGOS.crear_direccion";
            parametros.Clear();
            parametroOutput = new SqlParameter("@direccion_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@calle", calle));
            parametros.Add(new SqlParameter("@numero", this.siEstaVacioDevuelveDBNullSinoDecimal(numero)));
            parametros.Add(new SqlParameter("@piso", this.siEstaVacioDevuelveDBNullSinoDecimal(piso)));
            parametros.Add(new SqlParameter("@depto", departamento));
            parametros.Add(new SqlParameter("@cod_postal", codigoPostal));
            parametros.Add(new SqlParameter("@localidad", localidad));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idDireccion = (Decimal) parametroOutput.Value;

            // Si el cliente lo crea el admin, crea un nuevo usuario predeterminado. Si lo crea un nuevo registro de usuario, usa el que viene por parametro
            Decimal idUsuario;
            if (username == "clienteCreadoPorAdmin")
            {
                query = "LOS_SUPER_AMIGOS.crear_usuario";
                parametros.Clear();
                parametroOutput = new SqlParameter("@usuario_id", SqlDbType.Decimal);
                parametroOutput.Direction = ParameterDirection.Output;
                parametros.Add(parametroOutput);
                command = builderDeComandos.Crear(query, parametros);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                idUsuario = (Decimal)parametroOutput.Value;
            }
            else
            {
                query = "LOS_SUPER_AMIGOS.crear_usuario_con_valores";
                parametros.Clear();
                parametroOutput = new SqlParameter("@usuario_id", SqlDbType.Decimal);
                parametroOutput.Direction = ParameterDirection.Output;
                parametros.Add(new SqlParameter("@username", username));
                parametros.Add(new SqlParameter("@password", HashSha256.getHash(contrasena)));
                parametros.Add(parametroOutput);
                command = builderDeComandos.Crear(query, parametros);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                idUsuario = (Decimal)parametroOutput.Value;
            }
            
            // Hace el INSERT en Cliente
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", nombre));
            parametros.Add(new SqlParameter("@apellido", apellido));
            parametros.Add(new SqlParameter("@idTipoDeDocumento", idTipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", numeroDeDocumento));
            parametros.Add(new SqlParameter("@fechaDeNacimiento", this.siEstaVacioDevuelveDBNullSinoDecimal(fechaDeNacimiento)));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@telefono", telefono));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            parametros.Add(new SqlParameter("@idUsuario", idUsuario));

            query = "INSERT INTO LOS_SUPER_AMIGOS.Cliente (nombre, apellido, fecha_nacimiento, tipo_de_documento_id, documento, mail, telefono, direccion_id, usuario_id) values (@nombre, @apellido, @fechaDeNacimiento, @idTipoDeDocumento, @numeroDeDocumento, @mail, @telefono, @idDireccion, @idUsuario)";

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego el cliente correctamente");
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
                MessageBox.Show("Ya existe ese numero de documento");
                return false;
            }
            return true;
        }

        private bool pasoControlDeNoVacio(string valor)
        {
            if (valor == "")
            {
                MessageBox.Show("No se ingreso un " + valor);
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
    }
}
