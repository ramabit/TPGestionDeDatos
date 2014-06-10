using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Editar_Publicacion
{
    public partial class FiltrarPublicacion : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        
        public FiltrarPublicacion()
        {
            InitializeComponent();
        }

        private void FiltrarPublicacion_Load(object sender, EventArgs e)
        {
            CargarPublicacion();
            AgregarColumnaDeModificacion();
            AgregarListenerBotonDeModificacion();
            OcultarColumnasQueNoDebenVerse();
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Publicacion.Columns["id"].Visible = false;
        }

        private void CargarPublicacion()
        {
            command = builderDeComandos.Crear("SELECT p.id, u.username, p.tipo, p.estado, p.descripcion, p.fecha_inicio, p.fecha_vencimiento, r.descripcion, v.descripcion, p.se_realizan_preguntas, p.stock, p.precio FROM LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Rubro r, LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u WHERE p.rubro_id = r.id AND p.visibilidad_id = v.id AND p.usuario_id = u.id", parametros);

            DataSet publicaciones = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(publicaciones);
            dataGridView_Publicacion.DataSource = publicaciones.Tables[0].DefaultView;
        }

        private void AgregarColumnaDeModificacion()
        {
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "modificar";
            botonColumnaModificar.Name = "modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Publicacion.Columns.Add(botonColumnaModificar);
        }

        private void AgregarListenerBotonDeModificacion()
        {
            // Add a CellClick handler to handle clicks in the button column.
            dataGridView_Publicacion.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Publicacion_CellClick);
        }

        private void dataGridView_Publicacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Publicacion.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idPublicacionAModificiar = dataGridView_Publicacion.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new Editar_Publicacion.EditarPublicacion(idPublicacionAModificiar).ShowDialog();
            }
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = "";

            if (textBox_Descripcion.Text != "") filtro += "descripcion like '" + textBox_Descripcion.Text + "%'";

            query = "SELECT * FROM Publicacion WHERE " + filtro;

            command = builderDeComandos.Crear(query, parametros);

            DataSet publicaciones = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(publicaciones);
            dataGridView_Publicacion.DataSource = publicaciones.Tables[0].DefaultView;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            CargarPublicacion();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
