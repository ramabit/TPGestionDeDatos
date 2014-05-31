﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FrbaCommerce.Registro_de_Usuario
{
    public partial class RegistrarUsuario : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public Object SelectedItem { get; set; }

        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        private void RegistrarUsuario_Load(object sender, EventArgs e)
        {
            CargarRoles();
        }

        private void CargarRoles()
        {
            DataSet roles = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();
            command = builderDeComandos.Crear("SELECT distinct nombre FROM Rol  where habilitado = 1 and nombre != 'Administrador'", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(roles, "Rol");
            comboBoxRol.DataSource = roles.Tables[0].DefaultView;
            comboBoxRol.ValueMember = "nombre";
        }

        private void botonSiguiente_Click(object sender, EventArgs e)
        {

            String rolElegido = this.comboBoxRol.Text;
            String usuario = this.textBoxUsuario.Text;
            String contraseña = this.textBoxContraseña.Text;

            if (usuario == "")
            {
                MessageBox.Show("Debe completarse el campo Usuario");
            }

            if (contraseña == "")
            {
                MessageBox.Show("Debe completarse el campo Contraseña");
            }

            if (rolElegido == "")
            {
                MessageBox.Show("Debe seleccionarse un rol");
            }

            
            parametros.Clear();
            parametros.Add(new SqlParameter("@username", usuario));

            // Buscamos si el username ya se encuentra registrado
            String consulta = "SELECT id FROM Usuario WHERE username = @username";

            SqlDataReader reader = builderDeComandos.Crear(consulta, parametros).ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Ya existe un usuario con ese nombre");
            }

            

        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            new Login.LoginForm().Show();
            this.Close();
        }

       
    }
}
