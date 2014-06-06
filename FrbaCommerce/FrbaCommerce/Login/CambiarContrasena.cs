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
    public partial class CambiarContrasena : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public CambiarContrasena()
        {
            InitializeComponent();
        }

        private void botonContinuar_Click(object sender, EventArgs e)
        {
            if (textBoxContraseña.Text.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener por lo menos 8 caracteres");
                return;
            }

            if (textBoxContraseña.Text != textBoxRepetirContraseña.Text)
            {
                MessageBox.Show("La contraseña no se repite correctamente");
                return;
            }

            // Acualiza contraseña
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@username", UsuarioSesion.Usuario.nombre));
            parametros.Add(new SqlParameter("@pass", textBoxContraseña.Text));
            String nuevaPass = "update Usuario set password = @pass where username = @username";
            builderDeComandos.Crear(nuevaPass, parametros).ExecuteNonQuery();

            // Cambio el valor de primera sesion
            parametros.Clear();
            parametros.Add(new SqlParameter("@username", UsuarioSesion.Usuario.nombre));
            String primeraSesion = "update Usuario set primera_sesion = 0 where username = @username";
            builderDeComandos.Crear(primeraSesion, parametros).ExecuteNonQuery();

            // Asigna rol
            parametros.Clear();
            parametros.Add(new SqlParameter("@username", UsuarioSesion.Usuario.nombre));

            String consultaRoles = "select count(rol_id) from Rol_x_Usuario where habilitado = 1 and (select id from Usuario where username = @username) = usuario_id";
            int cantidadDeRoles = (int)builderDeComandos.Crear(consultaRoles, parametros).ExecuteScalar();

            if (cantidadDeRoles > 1)
            {
                new ElegirRol().ShowDialog();
                this.Hide();
            }
            else
            {
                parametros.Clear();
                parametros.Add(new SqlParameter("@username", UsuarioSesion.Usuario.nombre));
                String rolDeUsuario = "select r.nombre from Rol r, Rol_x_Usuario ru, Usuario u where r.id = ru.rol_id and ru.usuario_id = u.id and u.username = @username";
                String rolUser = (String)builderDeComandos.Crear(rolDeUsuario, parametros).ExecuteScalar();

                UsuarioSesion.Usuario.rol = rolUser;
                //  MessageBox.Show("Rol: " + UsuarioSesion.Usuario.rol);

                new MenuPrincipal().ShowDialog();
                this.Hide();
            }

        }


    }
}
