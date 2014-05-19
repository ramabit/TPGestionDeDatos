using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Login
{
    public partial class LoginForm : Form
    {

        private SqlCommand command { get; set; }
        ConexionDB conexion = new ConexionDB();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private SqlCommand CrearCommand(string sqlTexto, IList<SqlParameter> parametros)
        {
            this.command = new SqlCommand();
            this.command.CommandText = sqlTexto;
            if (parametros != null) { foreach (SqlParameter parametro in parametros) { this.command.Parameters.Add(parametro); } }
            if (this.command.Connection == null) this.command.Connection = conexion.AbrirConexion();

            return this.command;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void oCmbIngresar_Click(object sender, EventArgs e)
        {
            string query = "select * from Usuario where username = @username and password = @password";

            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@username", this.oTbxUsuario.Text));
            parametros.Add(new SqlParameter("@password", this.oTbxPass.Text));

            conexion.Reader = this.CrearCommand(query, parametros).ExecuteReader();

            if (conexion.PuedeLeer())
            {
                MessageBox.Show("Bienvenido " + conexion.Leer("username") + "!");
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos!");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
