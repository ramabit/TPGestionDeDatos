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
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        
        public Object SelectedItem { get; set; }

        public BajaForm()
        {
            InitializeComponent();
            llenacombobox();
        }

        private void BajaForm_Load(object sender, EventArgs e)
        {
        }

        public void llenacombobox()
        {
            DataSet roles = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();
            command = builderDeComandos.Crear("SELECT distinct nombre FROM Rol  where habilitado = 1", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(roles, "Rol");
            comboBox2.DataSource = roles.Tables[0].DefaultView;
            comboBox2.ValueMember = "nombre";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String rolElegido = this.comboBox2.Text;

            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", rolElegido));

            // Hacemos la baja logica del rol
            String sql = "UPDATE Rol SET habilitado = 0 WHERE nombre = @nombre";
           
            int filas_afectadas = 0;

            // ExecuteNonQuery devuelve la cantidad de filas que modifico
            filas_afectadas = builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
            if (filas_afectadas != -1)
            {
                MessageBox.Show("Deshabilitado rol " + rolElegido + "! Filas eliminadas: " + filas_afectadas);
            }
            else
            {
                MessageBox.Show("Error");
            }

            // Es necesario limpiar la lista de parametros
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", rolElegido));
            
            // Borramos el rol en los usuarios que lo tienen
            String sql2 = "UPDATE Rol_x_Usuario SET habilitado = 0 WHERE rol_id = (SELECT id FROM Rol WHERE nombre = @nombre)";
            
            filas_afectadas = builderDeComandos.Crear(sql2, parametros).ExecuteNonQuery();
            if (filas_afectadas != -1)
            {
                MessageBox.Show("Deshabilitado rol " + rolElegido + "! Filas eliminadas: " + filas_afectadas);
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
