using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.Gestion_de_Preguntas
{
    public partial class Respuesta : Form
    {
        public Respuesta(String pregunta, String respuesta)
        {
            InitializeComponent();
            labelPregunta.Text = pregunta;
            labelRespuesta.Text = respuesta;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
