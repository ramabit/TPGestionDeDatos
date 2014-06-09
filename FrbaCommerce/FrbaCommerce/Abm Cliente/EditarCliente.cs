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
    public partial class EditarCliente : Form
    {
        private String idCliente;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public EditarCliente(String idCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
        }

        private void EditarCliente_Load(object sender, EventArgs e)
        {
            CargarTipoDeDocumentos(); 
            CargarDatos();
        }

        private void CargarTipoDeDocumentos()
        {
            command = builderDeComandos.Crear("SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento", parametros);

            DataSet rubros = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(rubros);
            comboBox_TipoDeDocumento.DataSource = rubros.Tables[0].DefaultView;
            comboBox_TipoDeDocumento.ValueMember = "nombre";
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Cliente WHERE id = @idCliente";

            parametros.Clear();
            parametros.Add(new SqlParameter("@idCliente", idCliente));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();

            // Si no existe el id, tira error
            if (!reader.Read()) throw new Exception("No existe el cliente");

            textBox_Nombre.Text = Convert.ToString(reader["nombre"]);
            textBox_Apellido.Text = Convert.ToString(reader["apellido"]);
            query = "SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE id = @idTipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idTipoDeDocumento", reader["tipo_de_documento_id"]));
            String tipoDeDocumento = (String) builderDeComandos.Crear(query, parametros).ExecuteScalar();
            comboBox_TipoDeDocumento.SelectedValue = tipoDeDocumento;

            textBox_NumeroDeDoc.Text = Convert.ToString(reader["documento"]);

            textBox_FechaDeNacimiento.Text = Convert.ToString(reader["fecha_nacimiento"]);
            textBox_Mail.Text = Convert.ToString(reader["mail"]);
            textBox_Mail.Text = Convert.ToString(reader["telefono"]);

            query = "SELECT calle, numero, piso, depto, cod_postal, localidad FROM LOS_SUPER_AMIGOS.Direccion WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idDireccion", reader["direccion_id"]));
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
            String nombre = textBox_Nombre.Text;
            String apellido = textBox_Apellido.Text;
            String tipoDeDocumento = comboBox_TipoDeDocumento.Text;
            Decimal numeroDeDocumento = Convert.ToDecimal(textBox_NumeroDeDoc.Text);
            DateTime fechaDeNacimiento = Convert.ToDateTime(textBox_FechaDeNacimiento.Text);
            String mail = textBox_Mail.Text;
            Decimal telefono = Convert.ToDecimal(textBox_Telefono.Text);
            String calle = textBox_Calle.Text;
            String numero = textBox_Numero.Text;
            String piso = textBox_Piso.Text;
            String departamento = textBox_Departamento.Text;
            String codigoPostal = textBox_CodigoPostal.Text;
            String localidad = textBox_Localidad.Text;
            SqlParameter parametroOutput;

            // Averigua el id del tipo de documento a partir del nombre del tipo de documento
            query = "SELECT id FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE nombre = @tipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            Decimal idTipoDeDocumento = (Decimal)builderDeComandos.Crear(query, parametros).ExecuteScalar();

            // Crea una direccion y se guarda su id. Usa un stored procedure del script
            query = "LOS_SUPER_AMIGOS.crear_direccion";
            parametros.Clear();
            parametroOutput = new SqlParameter("@direccion_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@calle", calle));
            parametros.Add(new SqlParameter("@numero", numero));
            parametros.Add(new SqlParameter("@piso", piso));
            parametros.Add(new SqlParameter("@depto", departamento));
            parametros.Add(new SqlParameter("@cod_postal", codigoPostal));
            parametros.Add(new SqlParameter("@localidad", localidad));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idDireccion = (Decimal)parametroOutput.Value;

            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", nombre));
            parametros.Add(new SqlParameter("@apellido", apellido));
            parametros.Add(new SqlParameter("@idTipoDeDocumento", idTipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", numeroDeDocumento));
            parametros.Add(new SqlParameter("@fechaDeNacimiento", fechaDeNacimiento));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@telefono", telefono));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            parametros.Add(new SqlParameter("@idCliente", idCliente));

            query = "UPDATE LOS_SUPER_AMIGOS.Cliente set nombre = @nombre, apellido = @apellido, tipo_de_documento_id = @idTipoDeDocumento, documento = @numeroDeDocumento, fecha_nacimiento = @fechaDeNacimiento, mail = @mail, telefono = @telefono, direccion_id = @idDireccion, usuario_id WHERE id = @idCliente";

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego el cliente correctamente");
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
    }
}
