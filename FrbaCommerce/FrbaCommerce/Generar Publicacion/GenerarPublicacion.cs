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
            // CargarRubros();
            CargarVisibilidades();
        }

        private void CargarVisibilidades()
        {         
            command = builderDeComandos.Crear("SELECT descripcion FROM Visibilidad", parametros);
            
            DataSet visibilidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(visibilidades);
            comboBox_Visibilidad.DataSource = visibilidades.Tables[0].DefaultView;
            comboBox_Visibilidad.ValueMember = "descripcion";
        }

        private void CargarRubros()
        {
            command = builderDeComandos.Crear("SELECT nombre FROM Rubro", parametros);

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
            comboBox_tiposDePublicacion.DataSource = tiposDePublicacion;
            comboBox_tiposDePublicacion.ValueMember = "tipoDePublicacion";
        }
    }
}
