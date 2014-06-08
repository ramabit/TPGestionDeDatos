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
        private String usuario;
        private String contrasena;

        public AgregarCliente(String usuario, String contrasena)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.contrasena = contrasena;
        }

        private void AgregarCliente_Load(object sender, EventArgs e)
        {
            CargarTipoDeDocumentos();
        }

        private void CargarTipoDeDocumentos()
        {
            command = builderDeComandos.Crear("SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento tipoDoc", parametros);

            DataSet rubros = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(rubros);
            comboBox_TipoDeDocumento.DataSource = rubros.Tables[0].DefaultView;
            comboBox_TipoDeDocumento.ValueMember = "nombre";
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {

        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Nombre.Text = "";
            textBox_Apellido.Text = "";
            comboBox_TipoDeDocumento.SelectedIndex = 1;
            textBox_NumeroDeDoc.Text = "";
            textBox_FechaDeNacimiento.Text = "";
            textBox_Mail.Text = "";
            textBox_Calle.Text = "";
            textBox_Numero.Text = "";
            textBox_Piso.Text = "";
            textBox_Departamento.Text = "";
            textBox_CodigoPostal.Text = "";
        }      

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
