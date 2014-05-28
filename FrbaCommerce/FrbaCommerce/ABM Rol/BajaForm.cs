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
            if (parametros != null)
            { 
                foreach (SqlParameter parametro in parametros)
                { 
                    this.command.Parameters.Add(parametro); 
                }
            }
            if (this.command.Connection == null) this.command.Connection = conexion.AbrirConexion();

            return this.command;
        }

        public void llenacombobox()
        {
            DataSet roles = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct nombre FROM Rol where habilitado = 1", conexion.Conexion);
            IList<SqlParameter> parametros = new List<SqlParameter>();
            command = CrearCommand("SELECT distinct nombre FROM Rol  where habilitado = 1", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(roles, "Rol");
            comboBox2.DataSource = roles.Tables[0].DefaultView;
            comboBox2.ValueMember = "nombre";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String rolElegido = this.comboBox2.Text;
            MessageBox.Show(rolElegido);
            IList<SqlParameter> parametros = new List<SqlParameter>();
            
            parametros.Add(new SqlParameter("@nombre", rolElegido));

            String sql = "UPDATE Rol SET habilitado = 0 WHERE nombre = @nombre";
            // Esta consulta esta mal hecha. No se puede poner null una PK
            String sql2 = "UPDATE Rol_x_Usuario SET rol_id = null WHERE rol_id = (SELECT id FROM Rol WHERE nombre = @nombre)";

            int filas_afectadas = 0;

            // ExecuteNonQuery devuelve la cantidad de filas que modifico
            filas_afectadas = this.CrearCommand(sql, parametros).ExecuteNonQuery();
            if (filas_afectadas != -1)
            {
                MessageBox.Show("Deshabilitado rol " + rolElegido + "!" + filas_afectadas);
            }
            else
            {
                MessageBox.Show("Error");
            }

            // Es necesario limpiar la lista de parametros
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", rolElegido));

            filas_afectadas = this.CrearCommand(sql2, parametros).ExecuteNonQuery();
            if (filas_afectadas != -1)
            {
                MessageBox.Show("Deshabilitado rol " + rolElegido + "!" + filas_afectadas);
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
