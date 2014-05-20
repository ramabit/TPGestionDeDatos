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
        ConexionDB conexion = new ConexionDB();
        private SqlCommand command { get; set; }
        public Object SelectedItem { get; set; }

        public BajaForm()
        {
            InitializeComponent();
        }

        private void BajaForm_Load(object sender, EventArgs e)
        {
            llenacombobox();
        }

        public void llenacombobox()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter de = new SqlDataAdapter("SELECT distinct nombre FROM Rol", conexion);
            de.Fill(ds, "Rol");
            comboBox2.DataSource = ds.Tables[0].DefaultView;
            comboBox2.ValueMember = "nombre";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string itemRol = (string)comboBox2.SelectedItem;

            string sql = @"UPDATE Rol SET habilitado = 0 WHERE nombre = itemRol";
            SqlCommand command = new SqlCommand(sql, conexion);

            string sql1 = @"SELECT id WHERE nombre = itemRol";
            SqlCommand command1 = new SqlCommand(sql1, conexion);

            string sql2 = @"UPDATE Rol_x_Usuario SET Rol_id = null WHERE Rol_id = id";
            SqlCommand command2 = new SqlCommand(sql2, conexion);
        }
        
    }
}
