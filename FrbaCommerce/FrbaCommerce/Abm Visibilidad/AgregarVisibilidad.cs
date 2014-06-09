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
            Double precioPorPublicar = Convert.ToDouble(textBox_PrecioPorPublicar.Text);
            Decimal porcentajePorVenta = Convert.ToDecimal(textBox_PorcentajePorVenta.Text);

            query = "INSERT INTO LOS_SUPER_AMIGOS.Visibilidad (descripcion, precio, porcentaje) values (@descripcion, @precioPorPublicar, @porcentajePorVenta)";
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", descripcion));
            parametros.Add(new SqlParameter("@precioPorPublicar", precioPorPublicar));
            parametros.Add(new SqlParameter("@porcentajePorVenta", porcentajePorVenta));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se agrego la visibilidad correctamente");
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            textBox_PorcentajePorVenta.Text = "";
            textBox_PrecioPorPublicar.Text = "";
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
