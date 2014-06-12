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
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public Listado()
        {
            InitializeComponent();
        }

        private void Calificar_Load(object sender, EventArgs e)
        {
            CargarCompras();
        }

        private void CargarCompras()
        {
            
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            String comprasSinCalificar = "select c.id, u.username Vendedor, p.descripcion Publicacion, c.cantidad Cantidad," 
                        + " p.tipo Tipo, convert(varchar, c.fecha, 102) Fecha"
                        + " from LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Usuario u"
                        + " where c.usuario_id = @id and isnull(c.calificacion_id,0) = 0 and c.publicacion_id = p.id"
                        + " and p.usuario_id = u.id";
            command = builderDeComandos.Crear(comprasSinCalificar, parametros);

            DataSet datacompras = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(datacompras);

            dataGridViewCompras.AutoGenerateColumns = false;

            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Vendedor";
            col.HeaderText = "Vendedor";
            dataGridViewCompras.Columns.Add(col);
            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Publicacion";
            col2.HeaderText = "Descripcion de publicacion";
            dataGridViewCompras.Columns.Add(col2);
            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "Cantidad";
            col3.HeaderText = "Cantidad comprada";
            dataGridViewCompras.Columns.Add(col3);
            DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "Fecha";
            col4.HeaderText = "Fecha de compra";
            dataGridViewCompras.Columns.Add(col4);
            DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "Tipo";
            col5.HeaderText = "Tipo de publicacion";
            dataGridViewCompras.Columns.Add(col5);
            DataGridViewColumn col6 = new DataGridViewTextBoxColumn();
            col6.DataPropertyName = "id";
            col6.HeaderText = "id";
            dataGridViewCompras.Columns.Add(col6);

            dataGridViewCompras.Columns[5].Visible = false;

            dataGridViewCompras.DataSource = datacompras.Tables[0].DefaultView;

            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Calificar vendedor";
            botonColumnaModificar.Name = "Calificar vendedor";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridViewCompras.Columns.Add(botonColumnaModificar);

            dataGridViewCompras.CellClick += new DataGridViewCellEventHandler(dataGridViewCompras_CellClick);
            

        }

        private void dataGridViewCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo calificar vendedor
            if (e.ColumnIndex == dataGridViewCompras.Columns["Calificar vendedor"].Index && e.RowIndex >= 0)
            {
                Decimal idCompraParaCalificar = Convert.ToDecimal(dataGridViewCompras.Rows[e.RowIndex].Cells[5].Value);
                this.Hide();
                new Calificar(idCompraParaCalificar).ShowDialog();
                this.Close();
            }
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void dataGridViewCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
