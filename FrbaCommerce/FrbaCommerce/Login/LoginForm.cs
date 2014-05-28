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

        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void oCmbIngresar_Click(object sender, EventArgs e)
        {
            String query = "select * from Usuario where username = @username and password = @password";

            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@username", this.oTbxUsuario.Text));
            parametros.Add(new SqlParameter("@password", this.oTbxPass.Text));

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Bienvenido " + reader["username"] + "!");
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
