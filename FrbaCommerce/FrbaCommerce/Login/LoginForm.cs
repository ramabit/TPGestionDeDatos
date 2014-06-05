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
            String query = "select * from Usuario where username = @username and password = @password and habilitado = 1";

            String usuario = this.textBoxUsuario.Text;
            String contraseña = this.textBoxContaseña.Text;

            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@username", usuario));
            parametros.Add(new SqlParameter("@password", contraseña));

            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Bienvenido " + reader["username"] + "!");

                UsuarioSesion.Usuario.nombre = (String)reader["username"];
                UsuarioSesion.Usuario.id = (Decimal)reader["id"];

                parametros.Clear();
                parametros.Add(new SqlParameter("@username", this.textBoxUsuario.Text));

                String consulta = "select count(rol_id) from Rol_x_Usuario where habilitado = 1 and (select id from Usuario where username = @username) = usuario_id";
                int cantidadDeRoles = (int)builderDeComandos.Crear(consulta, parametros).ExecuteScalar();

                if(cantidadDeRoles > 1)
                {
                    new ElegirRol().Show();
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
                // Se fija si el usuario era correcto
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", this.textBoxUsuario.Text));
                String buscaUsuario = "select * from Usuario where username = @username";
                SqlDataReader lector = builderDeComandos.Crear(buscaUsuario, parametros).ExecuteReader();

                if (lector.Read())
                {
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    parametros.Add(new SqlParameter("@password", contraseña));
                    String estaDeshabilitado = "select * from Usuario where username = @username and habilitado = 0";

                    SqlDataReader leeDeshabilitado = builderDeComandos.Crear(estaDeshabilitado, parametros).ExecuteReader();

                    if (leeDeshabilitado.Read())
                    {
                        MessageBox.Show("El usuario esta deshabilitado");
                        return;
                    }

                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", this.textBoxUsuario.Text));
                    String sumaFallido = "update Usuario set login_fallidos = login_fallidos + 1 where username = @username";
                    builderDeComandos.Crear(sumaFallido, parametros).ExecuteNonQuery();

                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", this.textBoxUsuario.Text));
                    String cantidadFallidos = "select login_fallidos from Usuario where username = @username";
                    int intentosFallidos = (int)builderDeComandos.Crear(cantidadFallidos, parametros).ExecuteScalar();

                    if (intentosFallidos == 3)
                    {
                        parametros.Clear();
                        parametros.Add(new SqlParameter("@username", this.textBoxUsuario.Text));
                        String deshabilitar = "update Usuario set habilitado = 0 where username = @username";
                        builderDeComandos.Crear(deshabilitar, parametros).ExecuteNonQuery();
                    }
                    MessageBox.Show("Contraseña incorrecta. Fallidos del usuario: " + intentosFallidos);
                }
                else 
                {
                    MessageBox.Show("El usuario no existe");
                }
                
                
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
