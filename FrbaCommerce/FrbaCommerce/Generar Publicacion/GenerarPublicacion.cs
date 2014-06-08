using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Generar_Publicacion
{
    public partial class GenerarPublicacion : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public GenerarPublicacion()
        {
            InitializeComponent();
        }

        private void GenerarPublicacion_Load(object sender, EventArgs e)
        {
            CargarTiposDePublicacion();
            CargarRubros();
            CargarVisibilidades();
        }

        private void CargarVisibilidades()
        {
            command = builderDeComandos.Crear("SELECT descripcion FROM LOS_SUPER_AMIGOS.Visibilidad", parametros);
            
            DataSet visibilidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(visibilidades);
            comboBox_Visibilidad.DataSource = visibilidades.Tables[0].DefaultView;
            comboBox_Visibilidad.ValueMember = "descripcion";
        }

        private void CargarRubros()
        {
            command = builderDeComandos.Crear("SELECT nombre FROM LOS_SUPER_AMIGOS.Rubro", parametros);

            DataSet rubros = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(rubros);
            comboBox_Rubro.DataSource = rubros.Tables[0].DefaultView;
            comboBox_Rubro.ValueMember = "nombre";
        }

        private void CargarTiposDePublicacion()
        {
            DataTable tiposDePublicacion = new DataTable();
            tiposDePublicacion.Columns.Add("tipoDePublicacion");
            tiposDePublicacion.Rows.Add("Compra Inmediata");
            tiposDePublicacion.Rows.Add("Subasta");
            comboBox_TiposDePublicacion.DataSource = tiposDePublicacion;
            comboBox_TiposDePublicacion.ValueMember = "tipoDePublicacion";
        }

        private void comboBox_tiposDePublicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tipoSeleccionado = comboBox_TiposDePublicacion.Text;
            if (tipoSeleccionado == "Compra Inmediata")
            {
                label_stock.Text = "Stock";
                label_precio.Text = "Precio por unidad";
            }
            else
            {
                label_stock.Text = "Cantidad";
                label_precio.Text = "Precio inicial";
            }
        }

        private void button_generar_Click(object sender, EventArgs e)
        {
            String tipoSeleccionado = comboBox_TiposDePublicacion.Text;
            String descripcionSeleccionado = textBox_Descripcion.Text;
            String rubroSeleccionado = comboBox_Rubro.Text;
            String visibilidadSeleccionado = comboBox_Visibilidad.Text;
            Boolean preguntaSeleccionado = radioButton_Pregunta.Checked;
            Decimal stockSeleccionado = Convert.ToDecimal(textBox_Stock.Text);
            Double precioSeleccionado = Convert.ToDouble(textBox_Precio.Text);
            DateTime fecha = DateTime.Now;

            query = "SELECT id FROM LOS_SUPER_AMIGOS.Rubro WHERE nombre = @rubroSeleccionado";
            parametros.Clear();
            parametros.Add(new SqlParameter("@rubroSeleccionado", rubroSeleccionado));
            Decimal idRubroSeleccionado = (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();

            query = "SELECT id FROM LOS_SUPER_AMIGOS.Visibilidad WHERE descripcion = @visibilidadSeleccionado";
            parametros.Clear();
            parametros.Add(new SqlParameter("@visibilidadSeleccionado", visibilidadSeleccionado));
            Decimal idVisibilidadSeleccionado = (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();

            query = "INSERT INTO LOS_SUPER_AMIGOS.Publicacion (descripcion, stock, fecha_inicio, fecha_vencimiento, precio, rubro_id, visibilidad_id, usuario_id, estado, tipo) values (@descripcion, @stock, @fechaInicial, @fechaVencimiento, @precio, @rubroId, @visibilidadId, @usuarioId, 'Borrador', @tipo)";

            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", descripcionSeleccionado));
            parametros.Add(new SqlParameter("@stock", stockSeleccionado));
            parametros.Add(new SqlParameter("@fechaInicial", fecha));
            parametros.Add(new SqlParameter("@fechaVencimiento", fecha));
            parametros.Add(new SqlParameter("@precio", precioSeleccionado));
            parametros.Add(new SqlParameter("@rubroId", idRubroSeleccionado));
            parametros.Add(new SqlParameter("@visibilidadId", idVisibilidadSeleccionado));
            parametros.Add(new SqlParameter("@usuarioId", 1));
            parametros.Add(new SqlParameter("@tipo", tipoSeleccionado));
            
            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();
            
            if (filasAfectadas == 1) MessageBox.Show("Se agrego la nueva publicacion correctamente");
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
