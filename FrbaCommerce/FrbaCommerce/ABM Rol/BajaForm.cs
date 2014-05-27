using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FrbaCommerce.ABM_Rol
{

    public partial class BajaForm : Form
    {
        private SqlCommand command { get; set; }
        private ConexionDB conexion = new ConexionDB();
        
        public Object SelectedItem { get; set; }

        public BajaForm()
        {
            InitializeComponent();
            llenacombobox();
        }

        private void BajaForm_Load(object sender, EventArgs e)
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

        public void llenacombobox()
        {
            DataSet roles = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct nombre FROM Rol", conexion.Conexion);
            IList<SqlParameter> parametros = new List<SqlParameter>();
            command = CrearCommand("SELECT distinct nombre FROM Rol", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(roles, "Rol");
            comboBox2.DataSource = roles.Tables[0].DefaultView;
            comboBox2.ValueMember = "nombre";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", comboBox2.SelectedText));

            string sql = @"UPDATE Rol SET habilitado = 0 WHERE nombre = @nombre";

      //      string sql1 = @"SELECT id WHERE nombre = itemRol";
      //      SqlCommand command2 = new SqlCommand(sql1, conexion.Conexion);

      //      string sql2 = @"UPDATE Rol_x_Usuario SET Rol_id = null WHERE Rol_id = id";
      //      SqlCommand command3 = new SqlCommand(sql2, conexion.Conexion);

            conexion.Reader = this.CrearCommand(sql, parametros).ExecuteReader();

            if (conexion.PuedeLeer())
            {
                MessageBox.Show("Deshabilitado " + conexion.Leer("nombre") + "!");
            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
