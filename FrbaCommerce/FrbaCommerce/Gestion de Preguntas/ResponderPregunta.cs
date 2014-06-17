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
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;
        private int preguntaId;        

        public ResponderPregunta(int idPregunta, String pregunta)
        {
            InitializeComponent();
            preguntaId = idPregunta;
            labelPregunta.Text = pregunta;
        }

        private void ResponderPregunta_Load(object sender, EventArgs e)
        {

        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new VerPreguntas().ShowDialog();
            this.Close();
        }

        private void botonEnviar_Click(object sender, EventArgs e)
        {
            String sql = "UPDATE LOS_SUPER_AMIGOS.Pregunta SET respuesta = @respuesta, respuesta_fecha = @fecha WHERE id = @id";
            DateTime fecha = DateTime.Now;
            parametros.Clear();
            parametros.Add(new SqlParameter("@respuesta", this.textBoxRespuesta.Text));            
            parametros.Add(new SqlParameter("@id", preguntaId));
            parametros.Add(new SqlParameter("@fecha", fecha));
            builderDeComandos.Crear(sql, parametros).ExecuteNonQuery();
            MessageBox.Show("Respuesta enviada");
            this.Hide();
            new VerPreguntas().ShowDialog();
            this.Close();
        }
    }
}
