using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Facturar_Publicaciones
{
    public partial class Facturar : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public Facturar()
        {
            InitializeComponent();
        }

        private void Facturar_Load(object sender, EventArgs e)
        {

        }

        private void botonFacturar_Click(object sender, EventArgs e)
        {

        }
    }
}
