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


        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void botonIngresar_Click(object sender, EventArgs e)
        {
            String query = "select * from Usuario where username = @username and password = @password";

            String usuario = this.textBoxUsuario.Text;
            String contraseña = this.textBoxContaseña.Text;

            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@username", usuario));
            parametros.Add(new SqlParameter("@password", contraseña));

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Bienvenido " + reader["username"] + "!");
                
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", this.textBoxUsuario.Text));

                String consulta = "select count(rol_id) from Rol_x_Usuario where habilitado = 1 and (select id from Usuario where username = @username) = usuario_id";
                int cantidadDeRoles = (int)builderDeComandos.Crear(consulta, parametros).ExecuteScalar();

                if(cantidadDeRoles > 1)
                {
                    new ElegirRol(usuario).Show();
                    this.Hide();
                }
                else
                {
                    new MenuPrincipal().Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos!");
            }
        }

        private void textBoxContaseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void botonRegistrarse_Click(object sender, EventArgs e)
        {
            new Registro_de_Usuario.RegistrarUsuario().Show();
            this.Hide();
        }
    }
}
