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
    public partial class VerRespuestas : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;

        public VerRespuestas()
        {
            InitializeComponent();
        }

        private void VerRespuestas_Load(object sender, EventArgs e)
        {
            cargarPreguntas();
            hayRespuestas();
        }

        private void cargarPreguntas()
        {
            DataSet preguntas = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();            
            
            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));

            command = builderDeComandos.Crear("SELECT publicacion_id, descripcion, respuesta FROM LOS_SUPER_AMIGOS.Pregunta WHERE usuario_id = @usuario and respuesta != null", parametros);
            adapter.SelectCommand = command;
            adapter.Fill(preguntas);
            dataGridView1.DataSource = preguntas.Tables[0].DefaultView;
            dataGridView1.Columns[1].Visible = false;            
        }

        private void hayRespuestas()
        {
            if (dataGridView1.RowCount == 1)
            {
                MessageBox.Show("No hay respuestas");
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
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
            }

            dataGridView1.Columns.Add(buttons);

        }

        private void AgregarListenerBotonVerPublicacion()
        {
            dataGridView1.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idPublicacionElegida = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["publicacion_id"].Value);
            new DatosPublicacion(idPublicacionElegida).ShowDialog();            
        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }
    }
}
