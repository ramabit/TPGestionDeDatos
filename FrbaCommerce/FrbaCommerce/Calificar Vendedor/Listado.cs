using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Calificar_Vendedor
{
    public partial class Listado : Form
    {
        private ComunicadorConBaseDeDatos comunicar = new ComunicadorConBaseDeDatos();

        public Listado()
        {
            InitializeComponent();
        }

        private void Calificar_Load(object sender, EventArgs e)
        {
            CargarCompras();
            OcultarColumnasQueNoDebenVerse();
        }

        private void OcultarColumnasQueNoDebenVerse()
        {
            dataGridViewCompras.Columns["id"].Visible = false;
        }

        private void CargarCompras()
        {
            dataGridViewCompras.DataSource = comunicar.SelectDataTableConUsuario("c.id id, u.username Vendedor, p.descripcion Publicacion, c.cantidad Cantidad, tipo.descripcion Tipo, convert(varchar, c.fecha, 102) Fecha", "LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.TipoDePublicacion tipo", "tipo.id = p.tipo_id AND isnull(c.calificacion_id,0) = 0 AND c.publicacion_id = p.id and p.usuario_id = u.id AND c.usuario_id = @idUsuario");
            CargarColumnaCalificacion();
        }

        private void CargarColumnaCalificacion()
        {
            if (dataGridViewCompras.Columns.Contains("Calificar"))
                dataGridViewCompras.Columns.Remove("Calificar");
            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Calificar";
            botonColumnaModificar.Name = "Calificar";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridViewCompras.Columns.Add(botonColumnaModificar);
            dataGridViewCompras.CellClick +=
                new DataGridViewCellEventHandler(dataGridViewCompras_CellClick);
        }

        private void dataGridViewCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo calificar vendedor
            if (e.ColumnIndex == dataGridViewCompras.Columns["Calificar"].Index && e.RowIndex >= 0)
            {
                Decimal idCompraParaCalificar = Convert.ToDecimal(dataGridViewCompras.Rows[e.RowIndex].Cells[5].Value);
                new Calificar(idCompraParaCalificar).ShowDialog();
                CargarCompras();
            }
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

    }
}
