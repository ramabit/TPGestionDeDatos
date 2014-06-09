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
                MessageBox.Show("Debe ingresar un usuario");
                return;
            }

            if (this.textBoxContaseña.Text == "")
            {
                MessageBox.Show("Debe ingresar una contraseña");
                return;
            }


            // Nos fijamos si el usuario y contraseña existen y esta habilitado
            String query = "SELECT * FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @username AND password = @password AND habilitado = 1";

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

                // Setea fallidos de login de usuario a 0
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", usuario));
                String sumaFallido = "UPDATE LOS_SUPER_AMIGOS.Usuario SET login_fallidos = 0 WHERE username = @username";
                builderDeComandos.Crear(sumaFallido, parametros).ExecuteNonQuery();

                // Se fija si es el primer inicio de sesion del usuario
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", usuario));
                String sesion = "SELECT password FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @username";
                String primerInicio = (String)builderDeComandos.Crear(sesion, parametros).ExecuteScalar();
                if (primerInicio == "559aead08264d5795d3909718cdd05abd49572e84fe55590eef31a88a08fdffd")
                {
                    this.Hide();
                    new CambiarContrasena().ShowDialog();
                    this.Close();
                }

                parametros.Clear();
                parametros.Add(new SqlParameter("@username", usuario));

                String consultaRoles = "SELECT COUNT(rol_id) FROM LOS_SUPER_AMIGOS.Rol_x_Usuario WHERE (SELECT id FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @username) = usuario_id";
                int cantidadDeRoles = (int)builderDeComandos.Crear(consultaRoles, parametros).ExecuteScalar();

                if(cantidadDeRoles > 1)
                {
                    this.Hide();
                    new ElegirRol().ShowDialog();
                    this.Close();
                }
                else
                {
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    String rolDeUsuario = "SELECT r.nombre FROM LOS_SUPER_AMIGOS.Rol r, LOS_SUPER_AMIGOS.Rol_x_Usuario ru, LOS_SUPER_AMIGOS.Usuario u WHERE r.id = ru.rol_id AND ru.usuario_id = u.id AND u.username = @username";
                    String rolUser = (String)builderDeComandos.Crear(rolDeUsuario, parametros).ExecuteScalar();

                    UsuarioSesion.Usuario.rol = rolUser;
                  //  MessageBox.Show("Rol: " + UsuarioSesion.Usuario.rol);

                    this.Hide();
                    new MenuPrincipal().ShowDialog();
                    this.Close();
                }

            }
            else
            {
                // Se fija si el usuario era correcto
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", usuario));
                String buscaUsuario = "SELECT * FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @username";
                SqlDataReader lector = builderDeComandos.Crear(buscaUsuario, parametros).ExecuteReader();

                if (lector.Read())
                {

                    // Se fija si el usuario esta inhabilitado
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    parametros.Add(new SqlParameter("@password", contraseña));
                    String estaDeshabilitado = "SELECT * FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @username AND habilitado = 0";

                    SqlDataReader leeDeshabilitado = builderDeComandos.Crear(estaDeshabilitado, parametros).ExecuteReader();

                    if (leeDeshabilitado.Read())
                    {
                        MessageBox.Show("El usuario esta deshabilitado");
                        return;
                    }

                    // Suma un fallido
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    String sumaFallido = "UPDATE LOS_SUPER_AMIGOS.Usuario SET login_fallidos = login_fallidos + 1 WHERE username = @username";
                    builderDeComandos.Crear(sumaFallido, parametros).ExecuteNonQuery();


                    // Si es el tercer fallido se deshabilita al usuario
                    parametros.Clear();
                    parametros.Add(new SqlParameter("@username", usuario));
                    String cantidadFallidos = "SELECT login_fallidos FROM LOS_SUPER_AMIGOS.Usuario WHERE username = @username";
                    int intentosFallidos = (int)builderDeComandos.Crear(cantidadFallidos, parametros).ExecuteScalar();

                    if (intentosFallidos == 3)
                    {
                        parametros.Clear();
                        parametros.Add(new SqlParameter("@username", usuario));
                        String deshabilitar = "UPDATE LOS_SUPER_AMIGOS.Usuario SET habilitado = 0 WHERE username = @username";
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
            this.Hide();
            new Registro_de_Usuario.RegistrarUsuario().Show();
            this.Close();
        }
    }
}
