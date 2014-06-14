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
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();
        
        public FiltrarPublicacion()
        {
            InitializeComponent();
        }

        private void FiltrarPublicacion_Load(object sender, EventArgs e)
        {
            CargarPublicacion();
            OcultarColumnasQueNoDebenVerse();
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Publicacion.Columns["id"].Visible = false;
        }

        private void CargarPublicacion()
        {
            dataGridView_Publicacion.DataSource = comunicador.SelectPublicacionesParaFiltro();
            CargarColumnaModificacion();
        }

        private void CargarColumnaModificacion()
        {
            if (dataGridView_Publicacion.Columns.Contains("Modificar"))
                dataGridView_Publicacion.Columns.Remove("Modificar");
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Modificar";
            botonColumnaModificar.Name = "Modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Publicacion.Columns.Add(botonColumnaModificar);
            dataGridView_Publicacion.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Publicacion_CellClick);
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = CalcularFiltro();
            dataGridView_Publicacion.DataSource = comunicador.SelectPublicacionesParaFiltroConFiltro(filtro);
        }

        private String CalcularFiltro()
        {
            String filtro = "";
            return filtro;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            CargarPublicacion();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {

            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void dataGridView_Publicacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Publicacion.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idPublicacionAModificiar = dataGridView_Publicacion.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new Editar_Publicacion.EditarPublicacion(idPublicacionAModificiar).ShowDialog();
                CargarPublicacion();
            }
        }
    }
}
