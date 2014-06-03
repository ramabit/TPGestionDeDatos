using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Empresa
{
    public partial class EditarEmpresa : Form
    {
        private String idEmpresa;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public EditarEmpresa(String idEmpresa)
        {
            InitializeComponent();
            this.idEmpresa = idEmpresa;
        }

        private void EditarEmpresa_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            query = "SELECT * FROM Empresa WHERE id = @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            // Si no se puede leer tiro una excepcion
            if (!reader.Read()) throw new Exception("No se puede leer cliente");
            // Si se puede leer cargo los datos
            textBox_RazonSocial.Text = Convert.ToString(reader["razon_social"]);
            // textBox_NombreDeContacto.Text = Convert.ToString(reader[""]);
            textBox_Cuit.Text = Convert.ToString(reader["cuit"]);
            textBox_FechaDeCreacion.Text = Convert.ToString(reader["fecha_creacion"]);
            textBox_Mail.Text = Convert.ToString(reader["mail"]);
            // textBox_Calle.Text = Convert.ToString(reader["nombre"]);
            // textBox_Numero.Text = Convert.ToString(reader["nombre"]);
            // textBox_Piso.Text = Convert.ToString(reader["nombre"]);
            // textBox_Departamento.Text = Convert.ToString(reader["nombre"]);
            // textBox_CodigoPostal.Text = Convert.ToString(reader["nombre"]);
            if (Convert.ToInt32(reader["habilitado"]) == 1) checkBox_Habilitado.Checked = true;
        }
    }
}
