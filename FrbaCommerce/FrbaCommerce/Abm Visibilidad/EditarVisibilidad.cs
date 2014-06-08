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
    public partial class EditarVisibilidad : Form
    {
        private String idVisibilidad;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public EditarVisibilidad(String idVisibilidad)
        {
            InitializeComponent();
            this.idVisibilidad = idVisibilidad;
        }

        private void EditarVisibilidad_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Visibilidad WHERE id = @idVisibilidad";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idVisibilidad", idVisibilidad));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            // Si no se pudo leer, tira excepcion
            if (!reader.Read()) throw new Exception("No se puede leer visibilidad");
            // Si se puede leer, lo muestra en pantalla
            textBox_Descripcion.Text = Convert.ToString(reader["descripcion"]);
            textBox_PrecioPorPublicar.Text = Convert.ToString(reader["precio"]);
            textBox_PorcentajePorVenta.Text = Convert.ToString(reader["porcentaje"]);
            if (Convert.ToBoolean(reader["habilitado"])) checkBox_Habilitado.Checked = true;
        }
    }
}
