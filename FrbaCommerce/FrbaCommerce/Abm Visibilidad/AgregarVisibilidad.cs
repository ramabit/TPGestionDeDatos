using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Visibilidad
{
    public partial class AgregarVisibilidad : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public AgregarVisibilidad()
        {
            InitializeComponent();
        }

        private void AgregarVisibilidad_Load(object sender, EventArgs e)
        {

        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            String descripcion = textBox_Descripcion.Text;
            String precioPorPublicar = textBox_PrecioPorPublicar.Text;
            String porcentajePorVenta = textBox_PorcentajePorVenta.Text;

            // Controla que esten los campos numeroDeDocumento y telefono
            if (!this.pasoControlDeNoVacio(descripcion)) return;
            if (!this.pasoControlDeNoVacio(precioPorPublicar)) return;
            if (!this.pasoControlDeNoVacio(porcentajePorVenta)) return;

            // Controla que descripcion no se encuentren registrado en el sistema
            if (!this.pasoControlDeRegistro(descripcion)) return;

            query = "INSERT INTO LOS_SUPER_AMIGOS.Visibilidad (descripcion, precio, porcentaje) values (@descripcion, @precioPorPublicar, @porcentajePorVenta)";
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", descripcion));
            parametros.Add(new SqlParameter("@precioPorPublicar", Convert.ToDouble(precioPorPublicar)));
            parametros.Add(new SqlParameter("@porcentajePorVenta", Convert.ToDecimal(porcentajePorVenta)));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego la visibilidad correctamente");
        }

        private bool pasoControlDeNoVacio(string valor)
        {
            if (valor == "")
            {
                MessageBox.Show("Faltan datos");
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistro(String descripcion)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Visibilidad WHERE descripcion = @descripcion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", descripcion));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                MessageBox.Show("Ya existe esa descripcion");
                return false;
            }
            return true;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            textBox_PorcentajePorVenta.Text = "";
            textBox_PrecioPorPublicar.Text = "";
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }
    }
}
