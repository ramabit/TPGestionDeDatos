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
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public GenerarPublicacion()
        {
            InitializeComponent();
        }

        private void GenerarPublicacion_Load(object sender, EventArgs e)
        {
            CargarTiposDePublicacion();
        }

        private void CargarTiposDePublicacion()
        {
            DataTable tiposDePublicacion = new DataTable();
            tiposDePublicacion.Columns.Add("tipoDePublicacion");
            tiposDePublicacion.Rows.Add("Compra Inmediata");
            tiposDePublicacion.Rows.Add("Subasta");
            comboBox_tiposDePublicacion.DataSource = tiposDePublicacion;
        }
    }
}
