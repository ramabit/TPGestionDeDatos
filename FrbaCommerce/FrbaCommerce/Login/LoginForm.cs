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
            if (this.textBoxUsuario.Text == "")
            {
                MessageBox.Show("Debe ingresar un usario");
                return;
            }

            if (this.textBoxContaseña.Text == "")
            {
                MessageBox.Show("Debe ingresar una contraseña");
                return;
            }


            // Nos fijamos si el usuario y contraseña existen y esta habilitado
            String query = "select * from Usuario where username = @username and password = @password and habilitado = 1";

            String usuario = this.textBoxUsuario.Text;
            // encripta contraseña
            String contraseña = HashSha256.getHash(this.textBoxContaseña.Text);

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
                parametros.Add(new SqlParameter("@username", usuario));
                String sesion = "select primera_sesion from Usuario where username = @username";
                int primerInicio = (int)builderDeComandos.Crear(sesion, parametros).ExecuteScalar();
                if (primerInicio == 1)
                {
                    new CambiarContrasena().ShowDialog();
                    this.Hide();
                }

                parametros.Clear();
                parametros.Add(new SqlParameter("@username", usuario));

                String consultaRoles = "select count(rol_id) from Rol_x_Usuario where habilitado = 1 and (select id from Usuario where username = @username) = usuario_id";
                int cantidadDeRoles = (int)builderDeComandos.Crear(consultaRoles, parametros).ExecuteScalar();

                if(cantidadDeRoles > 1)
                {
                    new ElegirRol().ShowDialog();
                    this.Hide();
                }
                else
                {
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    String rolDeUsuario = "select r.nombre from Rol r, Rol_x_Usuario ru, Usuario u where r.id = ru.rol_id and ru.usuario_id = u.id and u.username = @username";
                    String rolUser = (String)builderDeComandos.Crear(rolDeUsuario, parametros).ExecuteScalar();

                    UsuarioSesion.Usuario.rol = rolUser;
                  //  MessageBox.Show("Rol: " + UsuarioSesion.Usuario.rol);

                    new MenuPrincipal().ShowDialog();
                    this.Hide();
                }

            }
            else
            {
                // Se fija si el usuario era correcto
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", usuario));
                String buscaUsuario = "select * from Usuario where username = @username";
                SqlDataReader lector = builderDeComandos.Crear(buscaUsuario, parametros).ExecuteReader();

                if (lector.Read())
                {

                    // Se fija si el usuario esta inhabilitado
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

                    // Suma un fallido
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    String sumaFallido = "update Usuario set login_fallidos = login_fallidos + 1 where username = @username";
                    builderDeComandos.Crear(sumaFallido, parametros).ExecuteNonQuery();


                    // Si es el tercer fallido se deshabilita al usuario
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    String cantidadFallidos = "select login_fallidos from Usuario where username = @username";
                    int intentosFallidos = (int)builderDeComandos.Crear(cantidadFallidos, parametros).ExecuteScalar();

                    if (intentosFallidos == 3)
                    {
                        parametros.Clear();
                        parametros.Add(new SqlParameter("@username", usuario));
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
