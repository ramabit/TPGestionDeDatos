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
    public partial class AgregarEmpresa : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private String username;
        private String contrasena;

        public AgregarEmpresa(String username, String contrasena)
        {
            InitializeComponent();
            this.username = username;
            this.contrasena = contrasena;
        }

        private void AgregarEmpresa_Load(object sender, EventArgs e)
        {
            AgregarListenerACalendario();
        }

        private void AgregarListenerACalendario()
        {
            this.monthCalendar_FechaDeCreacion.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_FechaDeCreacion_DateSelected);
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            // Guarda en variables todos los campos de entrada
            String razonSocial = textBox_RazonSocial.Text;
            String nombreDeContacto = textBox_NombreDeContacto.Text;
            String cuit = textBox_CUIT.Text;
            DateTime fechaDeCreacion = DateTime.Now;//Convert.ToDateTime(textBox_FechaDeCreacion.Text);
            String mail = textBox_Mail.Text;
            Decimal telefono = Convert.ToDecimal(textBox_Telefono.Text);
            String ciudad = textBox_Ciudad.Text;
            String calle = textBox_Calle.Text;
            String numero = textBox_Numero.Text;
            String piso = textBox_Piso.Text;
            String departamento = textBox_Departamento.Text;
            String codigoPostal = textBox_CodigoPostal.Text;
            String localidad = textBox_Localidad.Text;
            SqlParameter parametroOutput;

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

            // Crea un nuevo usuario y se guarda su id. Usa un stored procedure del script
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
            Decimal idUsuario = (Decimal)parametroOutput.Value;

            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            parametros.Add(new SqlParameter("@nombreDeContacto", nombreDeContacto));
            parametros.Add(new SqlParameter("@cuit", cuit));
            parametros.Add(new SqlParameter("@fechaDeCreacion", fechaDeCreacion));
            parametros.Add(new SqlParameter("@mail", mail));
            parametros.Add(new SqlParameter("@telefono", telefono));
            parametros.Add(new SqlParameter("@ciudad", ciudad));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            parametros.Add(new SqlParameter("@idUsuario", idUsuario));

            query = "INSERT INTO LOS_SUPER_AMIGOS.Empresa (razon_social, nombre_de_contacto, cuit, fecha_creacion, mail, telefono, ciudad, direccion_id, usuario_id) values (@razonSocial, @nombreDeContacto, @cuit, @fechaDeCreacion, @mail, @telefono, @ciudad, @idDireccion, @idUsuario)";

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego la nueva publicacion correctamente");
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_RazonSocial.Text = "";
            textBox_NombreDeContacto.Text = "";
            textBox_CUIT.Text = "";
            textBox_FechaDeCreacion.Text = "";
            textBox_Mail.Text = "";
            textBox_Telefono.Text = "";
            textBox_Ciudad.Text = "";
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

        private void button_FechaDeCreacion_Click(object sender, EventArgs e)
        {
            monthCalendar_FechaDeCreacion.Visible = true;
        }

        private void monthCalendar_FechaDeCreacion_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            textBox_FechaDeCreacion.Text = e.Start.ToShortDateString();
            monthCalendar_FechaDeCreacion.Visible = false;
        }
    }
}
