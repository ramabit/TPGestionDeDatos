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
            CargarDatos();
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Cliente WHERE id = @idCliente";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idCliente", idCliente));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (!reader.Read()) throw new Exception("No se puede leer cliente");
            textBox_Nombre.Text = Convert.ToString(reader["nombre"]);
            textBox_Apellido.Text = Convert.ToString(reader["apellido"]);
            textBox_Dni.Text = Convert.ToString(reader["dni"]);
            textBox_FechaDeNacimiento.Text = Convert.ToString(reader["fecha_nacimiento"]);
            textBox_Mail.Text = Convert.ToString(reader["mail"]);
            // textBox_Calle.Text = Convert.ToString(reader["nombre"]);
            // textBox_Numero.Text = Convert.ToString(reader["nombre"]);
            // textBox_Piso.Text = Convert.ToString(reader["nombre"]);
            // textBox_Departamento.Text = Convert.ToString(reader["nombre"]);
            // textBox_CodigoPostal.Text = Convert.ToString(reader["nombre"]);
            if (Convert.ToBoolean(reader["habilitado"])) checkBox_Habilitado.Checked = true; 
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {

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
            this.Close();
        }

        private void button_FechaDeNacimiento_Click(object sender, EventArgs e)
        {
            this.monthCalendar_FechaDeNacimiento.Visible = true;
        }
    }
}
