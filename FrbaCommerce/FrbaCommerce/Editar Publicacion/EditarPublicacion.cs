using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Editar_Publicacion
{
    public partial class EditarPublicacion : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private String idPublicacion;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public EditarPublicacion(String idPublicacion)
        {
            InitializeComponent();
            this.idPublicacion = idPublicacion;
        }

        private void EditarPublicacion_Load(object sender, EventArgs e)
        {
            CargarTiposDePublicacion();
            CargarEstados();
            CargarRubros();
            CargarVisibilidades();
            CargarDatos();
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

        private void CargarEstados()
        {
            DataTable estados = new DataTable();
            estados.Columns.Add("estados");

            query = "SELECT estado FROM LOS_SUPER_AMIGOS.Publicacion WHERE id = @idPublicacion";

            parametros.Clear();
            parametros.Add(new SqlParameter("@idPublicacion", idPublicacion));
            String estado = (String) builderDeComandos.Crear(query, parametros).ExecuteScalar();

            if (estado == "Borrador") CargarSegunBorrador(estados);
            if (estado == "Publicada") CargarSegunPublicada(estados);
            if (estado == "Pausada") CargarSegunPausada(estados);

            comboBox_Estado.DataSource = estados;
            comboBox_Estado.ValueMember = "estados";
        }

        private void CargarSegunBorrador(DataTable estados)
        {
            estados.Rows.Add("Borrador");
            estados.Rows.Add("Publicada");
            comboBox_TiposDePublicacion.Enabled = false;
        }

        private void CargarSegunPublicada(DataTable estados)
        {
            estados.Rows.Add("Publicada");
            estados.Rows.Add("Pausada");
            estados.Rows.Add("Finalizada");
            textBox_Descripcion.Enabled = false;
            textBox_FechaDeInicio.Enabled = false;
            button_FechaDeInicio.Enabled = false;
            comboBox_Rubro.Enabled = false;
            comboBox_Visibilidad.Enabled = false;
            comboBox_TiposDePublicacion.Enabled = false;
        }

        private void CargarSegunPausada(DataTable estados)
        {
            estados.Rows.Add("Publicada");
            estados.Rows.Add("Pausada");
            estados.Rows.Add("Finalizada");
            textBox_Descripcion.Enabled = false;
            textBox_FechaDeInicio.Enabled = false;
            button_FechaDeInicio.Enabled = false;
            comboBox_Rubro.Enabled = false;
            comboBox_Visibilidad.Enabled = false;
            comboBox_TiposDePublicacion.Enabled = false;
            textBox_Precio.Enabled = false;
            textBox_Stock.Enabled = false;
        }

        private void CargarRubros()
        {
            comboBox_Rubro.DataSource = comunicador.SelectDataTable("descripcion", "Rubro");
            comboBox_Rubro.ValueMember = "descripcion";
        }

        private void CargarVisibilidades()
        {
            comboBox_Visibilidad.DataSource = comunicador.SelectDataTable("descripcion", "Visibilidad");
            comboBox_Visibilidad.ValueMember = "descripcion";
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE id = @idPublicacion";

            parametros.Clear();
            parametros.Add(new SqlParameter("@idPublicacion", idPublicacion));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();

            // Si no se puede leer tiro una excepcion
            if (!reader.Read()) throw new Exception("No se puede leer publicacion");

            textBox_Descripcion.Text = Convert.ToString(reader["descripcion"]);
            textBox_FechaDeInicio.Text = Convert.ToString(reader["fecha_inicio"]);
            textBox_Precio.Text = Convert.ToString(reader["precio"]);
            textBox_Stock.Text = Convert.ToString(reader["stock"]);

            

            query = "SELECT descripcion FROM LOS_SUPER_AMIGOS.Rubro WHERE id = @idRubro";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idRubro", Convert.ToDecimal(reader["rubro_id"])));
            String rubro = (String) builderDeComandos.Crear(query, parametros).ExecuteScalar();
            comboBox_Rubro.SelectedValue = rubro;

            query = "SELECT descripcion FROM LOS_SUPER_AMIGOS.Visibilidad WHERE id = @idVisibilidad";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idVisibilidad", Convert.ToDecimal(reader["visibilidad_id"])));
            String visibilidad = (String)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            comboBox_Visibilidad.SelectedValue = visibilidad;

            comboBox_TiposDePublicacion.SelectedValue = Convert.ToString(reader["tipo"]);
            comboBox_Estado.SelectedValue = Convert.ToString(reader["estado"]);
            radioButton_Pregunta.Checked = Convert.ToBoolean(reader["se_realizan_preguntas"]);
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            String tipoSeleccionado = comboBox_TiposDePublicacion.Text;
            String estado = comboBox_Estado.Text;
            String descripcionSeleccionado = textBox_Descripcion.Text;
            String fechaDeInicio = textBox_FechaDeInicio.Text;
            String rubroSeleccionado = comboBox_Rubro.Text;
            String visibilidadSeleccionado = comboBox_Visibilidad.Text;
            Boolean preguntaSeleccionado = radioButton_Pregunta.Checked;
            String stockSeleccionado = textBox_Stock.Text;
            String precioSeleccionado = textBox_Precio.Text;

            // Controla que esten los campos numeroDeDocumento y telefono
            if (!this.pasoControlDeNoVacio(descripcionSeleccionado)) return;
            if (!this.pasoControlDeNoVacio(fechaDeInicio)) return;
            if (!this.pasoControlDeNoVacio(stockSeleccionado)) return;
            if (!this.pasoControlDeNoVacio(precioSeleccionado)) return;

            query = "SELECT id FROM LOS_SUPER_AMIGOS.Rubro WHERE descripcion = @rubroSeleccionado";
            parametros.Clear();
            parametros.Add(new SqlParameter("@rubroSeleccionado", rubroSeleccionado));
            Decimal idRubroSeleccionado = (Decimal)builderDeComandos.Crear(query, parametros).ExecuteScalar();

            query = "SELECT * FROM LOS_SUPER_AMIGOS.Visibilidad WHERE descripcion = @visibilidadSeleccionado";
            parametros.Clear();
            parametros.Add(new SqlParameter("@visibilidadSeleccionado", visibilidadSeleccionado));
            SqlDataReader readerVisibilidad = builderDeComandos.Crear(query, parametros).ExecuteReader();
            readerVisibilidad.Read();

            query = "UPDATE LOS_SUPER_AMIGOS.Publicacion SET estado = @estado, descripcion = @descripcion, fecha_inicio = @fechaDeInicio, rubro_id = @rubroId, visibilidad_id = @visibilidadId, se_realizan_preguntas = @preguntaSeleccionado, stock = @stock, precio = @precio WHERE id = @idPublicacion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idPublicacion", idPublicacion));
            parametros.Add(new SqlParameter("@estado", estado));
            parametros.Add(new SqlParameter("@descripcion", descripcionSeleccionado));
            parametros.Add(new SqlParameter("@fechaDeInicio", fechaDeInicio));
            parametros.Add(new SqlParameter("@fechaVencimiento", Convert.ToDateTime(fechaDeInicio).AddDays(Convert.ToDouble(readerVisibilidad["duracion"])))); // OJO ACA
            parametros.Add(new SqlParameter("@stock", Convert.ToDecimal(stockSeleccionado)));
            parametros.Add(new SqlParameter("@precio", Convert.ToDouble(precioSeleccionado)));
            parametros.Add(new SqlParameter("@rubroId", idRubroSeleccionado));
            parametros.Add(new SqlParameter("@visibilidadId", readerVisibilidad["id"]));
            parametros.Add(new SqlParameter("@preguntaSeleccionado", preguntaSeleccionado));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) MessageBox.Show("Se modifico la nueva publicacion correctamente");

            this.Close();
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

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
