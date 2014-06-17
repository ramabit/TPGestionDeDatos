using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FrbaCommerce.Gestion_de_Preguntas
{
    public partial class ResponderPregunta : Form
    {
        int publicacionId;

        public ResponderPregunta(int idPublicacion)
        {
            InitializeComponent();
            publicacionId = idPublicacion;
        }

        private void ResponderPregunta_Load(object sender, EventArgs e)
        {

        }
    }
}
