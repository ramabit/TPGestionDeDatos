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
<<<<<<< HEAD
         //   textBox_Dni.Text = Convert.ToString(reader["dni"]);
=======

            query = "SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE id = @idTipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idTipoDeDocumento", reader["tipo_de_documento_id"]));
            String tipoDeDocumento = (String) builderDeComandos.Crear(query, parametros).ExecuteScalar();
            comboBox_TipoDeDocumento.SelectedValue = tipoDeDocumento;

            textBox_NumeroDeDoc.Text = Convert.ToString(reader["documento"]);
>>>>>>> 061fa6155146604dad6d26cd87611355cad6c212
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
