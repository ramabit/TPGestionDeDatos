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
            String nombre = textBox_Nombre.Text;
            String apellido = textBox_Apellido.Text;
            String tipoDeDocumento = comboBox_TipoDeDocumento.Text;
            Decimal numeroDeDocumento = Convert.ToDecimal(textBox_NumeroDeDoc.Text);
            DateTime fechaDeNacimiento = DateTime.Now; //textBox_FechaDeNacimiento.Text;
            String mail = textBox_Mail.Text;
            String calle = textBox_Calle.Text;
            String numero = textBox_Numero.Text;
            String piso = textBox_Piso.Text;
            String departamento = textBox_Departamento.Text;
            String codigoPostal = textBox_CodigoPostal.Text;

            query = "SELECT id FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE nombre = @tipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            Decimal idTipoDeDocumento = (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();

            query = "LOS_SUPER_AMIGOS.crear_direccion";
            parametros.Clear();
            SqlParameter parametro1 = new SqlParameter("@calle", SqlDbType.NVarChar, 100);
            parametro1.Value = calle;
            SqlParameter parametro2 = new SqlParameter("@numero", SqlDbType.Decimal);
            parametro2.Value = numero;
            SqlParameter parametro3 = new SqlParameter("@piso", SqlDbType.Decimal);
            parametro3.Value = piso;
            SqlParameter parametro4 = new SqlParameter("@depto", SqlDbType.NVarChar, 5);
            parametro4.Value = departamento;
            SqlParameter parametro5 = new SqlParameter("@cod_postal", SqlDbType.NVarChar, 50);
            parametro5.Value = codigoPostal;
            SqlParameter parametro6 = new SqlParameter("@direccion_id", SqlDbType.Decimal);
            parametro6.Direction = ParameterDirection.Output;
            parametros.Add(parametro1);
            parametros.Add(parametro2);
            parametros.Add(parametro3);
            parametros.Add(parametro4);
            parametros.Add(parametro5);
            parametros.Add(parametro6);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idDireccion = (Decimal) parametro6.Value;

            query = "SELECT id FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @usuario";
            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", usuario));
            Decimal idUsuario = (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();
            
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", nombre));
            parametros.Add(new SqlParameter("@apellido", apellido));
            parametros.Add(new SqlParameter("@idTipoDeDocumento", idTipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", numeroDeDocumento));
            parametros.Add(new SqlParameter("@fechaDeNacimiento", fechaDeNacimiento));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            parametros.Add(new SqlParameter("@idUsuario", idUsuario));

            query = "INSERT INTO LOS_SUPER_AMIGOS.Cliente (nombre, apellido, fecha_nacimiento, tipo_de_documento_id, documento, mail, direccion_id, usuario_id) values (@nombre, @apellido, @fechaDeNacimiento, @idTipoDeDocumento, @numeroDeDocumento, @mail, @idDireccion, @idUsuario)";

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego la nueva publicacion correctamente");
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Nombre.Text = "";
            textBox_Apellido.Text = "";
            comboBox_TipoDeDocumento.SelectedIndex = 0;
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
