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
            String comprasSinCalificar = "CREATE Table LOS_SUPER_AMIGOS.Compras_Sin_Calificar (id numeric(18,0), tipo nvarchar(10)) insert LOS_SUPER_AMIGOS.Compras_Sin_Calificar (id, tipo) (select id, 'Compra' as tipo from LOS_SUPER_AMIGOS.Compra where usuario_id = @id and isnull(calificacion_id,0) = 0) UNION (select id, 'Ofertas' as tipo from LOS_SUPER_AMIGOS.Oferta where usuario_id = @id and isnull(calificacion_id,0) = 0 and gano_subasta = 1)";
            builderDeComandos.Crear(comprasSinCalificar, parametros).ExecuteNonQuery();

            parametros.Clear();
            String compras = "(select csc.id id, csc.tipo tipo, u.username Vendedor, p.descripcion Publicacion," 
                    + " c.cantidad Cantidad, convert(varchar, c.fecha, 101) Fecha"
                    + " from LOS_SUPER_AMIGOS.Compras_Sin_Calificar csc, LOS_SUPER_AMIGOS.Compra c," 
                    + " LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.Publicacion p"
                    + " where csc.tipo = 'Compra' and csc.id = c.id and p.usuario_id = u.id  and"
                    + " c.publicacion_id = p.id)"
                    + " union all"
                    + " (select csc.id, csc.tipo, u.username Vendedor, p.descripcion Publicacion," 
                    + " p.stock Cantidad, convert(varchar, o.fecha, 101) Fecha"
                    + " from LOS_SUPER_AMIGOS.Compras_Sin_Calificar csc, LOS_SUPER_AMIGOS.Oferta o," 
                    + " LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.Publicacion p"
                    + " where csc.tipo = 'Oferta' and csc.id = o.id and p.usuario_id = u.id and"
                    + " o.publicacion_id = p.id)";
            command = builderDeComandos.Crear(compras, parametros);

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
            col5.DataPropertyName = "tipo";
            col5.HeaderText = "Tipo";
            dataGridViewCompras.Columns.Add(col5);
            DataGridViewColumn col6 = new DataGridViewTextBoxColumn();
            col6.DataPropertyName = "id";
            col6.HeaderText = "id";
            dataGridViewCompras.Columns.Add(col6);

         //   dataGridViewCompras.Columns[5].Visible = false;

            dataGridViewCompras.DataSource = datacompras.Tables[0].DefaultView;

            DataGridViewButtonColumn botonColumnaModificar = new DataGridViewButtonColumn();
            botonColumnaModificar.Text = "Calificar vendedor";
            botonColumnaModificar.Name = "Calificar vendedor";
            botonColumnaModificar.UseColumnTextForButtonValue = true;
            dataGridViewCompras.Columns.Add(botonColumnaModificar);

            dataGridViewCompras.CellClick += new DataGridViewCellEventHandler(dataGridViewCompras_CellClick);
            
            
            parametros.Clear();
            String sacamosTabla = "drop table LOS_SUPER_AMIGOS.Compras_Sin_Calificar";
            builderDeComandos.Crear(sacamosTabla,parametros).ExecuteNonQuery();

        }

        private void dataGridViewCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Controla que la celda que se clickeo calificar vendedor
            if (e.ColumnIndex == dataGridViewCompras.Columns["Calificar vendedor"].Index && e.RowIndex >= 0)
            {
                String idCompraParaCalificar = dataGridViewCompras.Rows[e.RowIndex].Cells["id"].Value.ToString();
                String tipoCompraParaCalificar = dataGridViewCompras.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                new Calificar(idCompraParaCalificar, tipoCompraParaCalificar).ShowDialog();
            }
        }

    }
}
