using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.ABM_Visibilidad
{
    public partial class FiltroVisibilidad : Form
    {
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public FiltroVisibilidad()
        {
            InitializeComponent();
        }

        private void FiltroVisibilidad_Load(object sender, EventArgs e)
        {
            CargarVisibilidad();
            OcultarColumnasQueNoDebenVerse();
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridView_Visibilidad.Columns["id"].Visible = false;
        }

        private void CargarVisibilidad()
        {
            dataGridView_Visibilidad.DataSource = comunicador.SelectVisibilidadesParaFiltro();
            CargarColumnaModificacion();
        }

        private void CargarColumnaModificacion()
        {
            if (dataGridView_Visibilidad.Columns.Contains("Modificar"))
                dataGridView_Visibilidad.Columns.Remove("Modificar");
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Modificar";
            botonColumnaModificar.Name = "Modificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridView_Visibilidad.Columns.Add(botonColumnaModificar);
            dataGridView_Visibilidad.CellClick +=
                new DataGridViewCellEventHandler(dataGridView_Visibilidad_CellClick);
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String filtro = CalcularFiltro();
            dataGridView_Visibilidad.DataSource = comunicador.SelectVisibilidadesParaFiltroConFiltro(filtro);
        }

        private String CalcularFiltro()
        {
            String filtro = "";
            if (textBox_Descripcion.Text != "") filtro += "descripcion like '" + textBox_Descripcion.Text + "%'";
            return filtro;
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            CargarVisibilidad();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void dataGridView_Visibilidad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo fue la de modificar
            if (e.ColumnIndex == dataGridView_Visibilidad.Columns["modificar"].Index && e.RowIndex >= 0)
            {
                String idVisibilidadAModificiar = dataGridView_Visibilidad.Rows[e.RowIndex].Cells["id"].Value.ToString();
                new EditarVisibilidad(idVisibilidadAModificiar).ShowDialog();
                CargarVisibilidad();
            }
        }
    }
}
