using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Rubro
{
    public partial class EditarRubro : Form
    {
        private String idRubro;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public EditarRubro(String idRubro)
        {
            InitializeComponent();
            this.idRubro = idRubro;
        }

        private void EditarRubro_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM Rubro WHERE id = @idRubro";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idRubro", idRubro));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            // Si no se pudo leer, tira excepcion
            if (!reader.Read()) throw new Exception("No se puede leer rubro");
            // Si se puede leer, lo muestra en pantalla
            textBox_Descripcion.Text = Convert.ToString(reader["descripcion"]);
            if (Convert.ToInt32(reader["habilitado"]) == 1) checkBox_Habilitado.Checked = true;
        }
    }
}
