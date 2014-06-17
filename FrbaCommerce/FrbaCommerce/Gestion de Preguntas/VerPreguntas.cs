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
    public partial class VerPreguntas : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;

        public VerPreguntas()
        {
            InitializeComponent();
        }

        private void VerPreguntas_Load_1(object sender, EventArgs e)
        {
            cargarPreguntas();
            hayPreguntas();
        }

        private void cargarPreguntas()
        {
            DataSet preguntas = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();

            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));            
            command = builderDeComandos.Crear("SELECT pregunta.publicacion_id, pregunta.id, pregunta.descripcion FROM LOS_SUPER_AMIGOS.Pregunta pregunta, LOS_SUPER_AMIGOS.Publicacion publicacion WHERE pregunta.respuesta = '' and pregunta.publicacion_id = publicacion.id and publicacion.usuario_id = @usuario", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(preguntas);
            dataGridView1.DataSource = preguntas.Tables[0].DefaultView;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            AgregarBotonDatosPublicacion();
            AgregarBotonResponder();
            AgregarListenerBotones();
        }

        private void hayPreguntas()
        {
            if (dataGridView1.RowCount == 1)
            {
                MessageBox.Show("No hay preguntas para responder");
                this.Hide();
                new MenuPrincipal().ShowDialog();
                this.Close();
            }
        }

        private void AgregarBotonDatosPublicacion()
        {
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Datos Publicacion";
                buttons.Text = "Datos Publicacion";
                buttons.Name = "Datos Publicacion";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
            }

            dataGridView1.Columns.Add(buttons);
        }

        private void AgregarBotonResponder()
        {
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Responder";
                buttons.Text = "Responder";
                buttons.Name = "Responder";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
            }

            dataGridView1.Columns.Add(buttons);
        }

        private void AgregarListenerBotones()
        {
            dataGridView1.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idPublicacionElegida = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["publicacion_id"].Value);
            int idPregunta = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
            String pregunta = dataGridView1.Rows[e.RowIndex].Cells["descripcion"].Value.ToString();

            if (e.ColumnIndex == dataGridView1.Columns["Datos Publicacion"].Index)
            {                
                new DatosPublicacion(idPublicacionElegida).ShowDialog();
            }

            if (e.ColumnIndex == dataGridView1.Columns["Responder"].Index)
            {
                this.Hide();
                new ResponderPregunta(idPregunta, pregunta).ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }        
    }
}
