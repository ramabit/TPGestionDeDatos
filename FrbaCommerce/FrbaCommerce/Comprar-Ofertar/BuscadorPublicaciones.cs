using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FrbaCommerce.Comprar_Ofertar
{
    public partial class BuscadorPublicaciones : Form
    {
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        public Object SelectedItem { get; set; }
        decimal idUsuarioActual = UsuarioSesion.Usuario.id;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        DataTable tablaTemporal;
        int totalPaginas;
        int totalPublicaciones;
        int publicacionesPorPagina = 10;
        int paginaActual;
        int ini;
        int fin;

        public BuscadorPublicaciones()
        {
            InitializeComponent();
        }
                
        private void BuscardorPublicaciones_Load(object sender, EventArgs e)
        {            
            CargarRubros();            
        }

        private void CargarRubros()
        {
            comboBoxRubro.DataSource = comunicador.SelectDataTable("descripcion", "LOS_SUPER_AMIGOS.Rubro", "habilitado = 1");
            comboBoxRubro.ValueMember = "descripcion";
            comboBoxRubro.SelectedIndex = -1;
        }

        private void botonBuscar_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            parametros = new List<SqlParameter>();
            parametros.Clear();
            parametros.Add(new SqlParameter("@usuario", idUsuarioActual));
            DataTable busquedaTemporal = new DataTable();
            String filtro = "and publicacion.usuario_id != @usuario";            

            if (textBoxDescripcion.Text != "")
            {
                filtro += " and publicacion.descripcion like '%" + textBoxDescripcion.Text + "%'";                
            }            

            if (comboBoxRubro.Text != "")
            {
                String idRubro = Convert.ToString(comunicador.SelectFromWhere("id", "Rubro", "descripcion", comboBoxRubro.Text));
                parametros.Add(new SqlParameter("@idRubro", idRubro));
                filtro += " and publicacion.rubro_id = @idRubro";                
            }

            String query = "SELECT publicacion.id, publicacion.descripcion, publicacion.precio, publicacion.tipo FROM LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Visibilidad visibilidad WHERE publicacion.visibilidad_id = visibilidad.id AND (publicacion.estado = 'Publicada' or publicacion.estado = 'Pausada') " + filtro + " ORDER BY visibilidad.precio DESC";
            
            command = builderDeComandos.Crear(query, parametros);
            adapter.SelectCommand = command;            
            adapter.Fill(busquedaTemporal);

            int cantFilas = busquedaTemporal.Rows.Count;
            if (cantFilas == 0)
            {
                MessageBox.Show("No hay resultados");
            }
            else
            {
                tablaTemporal = busquedaTemporal;
                calcularPaginas();
                ini = 0;
                if (totalPublicaciones > 9)
                {
                    fin = 9;
                }
                else
                {
                    fin = totalPublicaciones;
                }
                calcularPaginas();
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                dataGridView1.Columns[0].Visible = false;
                mostrarNrosPaginas(ini);
            }
            AgregarBotonVerPublicacion();
        }

        private void calcularPaginas()
        {
            totalPublicaciones = tablaTemporal.Rows.Count - 1;
            totalPaginas = totalPublicaciones / publicacionesPorPagina;
            if ((totalPublicaciones / publicacionesPorPagina) > 0)
            {
                totalPaginas += 1;
            }
        }

        private DataTable paginarDataGridView(int ini, int fin)
        {
            DataTable publicacionesDeUnaPagina = new DataTable();
            publicacionesDeUnaPagina = tablaTemporal.Clone();
            for (int i = ini; i <= fin; i++)
            {
                publicacionesDeUnaPagina.ImportRow(tablaTemporal.Rows[i]);
            }
            return publicacionesDeUnaPagina;
        }

        private void mostrarNrosPaginas(int ini)
        {
            paginaActual = (ini / publicacionesPorPagina) + 1;
            labelNrosPagina.Text = "Pagina " + paginaActual + "/" + totalPaginas;
        }        

        private bool VerificarSiSeBusco()
        {
            if (totalPaginas == 0)
            {
                MessageBox.Show("Aun no buscaste nada");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool sePuedeRetrocederPaginas()
        {
            if (VerificarSiSeBusco() == false)
            {
                return false;
            }
            else
            {
                if (paginaActual == 1)
                {
                    MessageBox.Show("Ya estas en la 1º pagina");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void botonPrimeraPagina_Click(object sender, EventArgs e)
        {
            if (sePuedeRetrocederPaginas())
            {
                ini = 0;
                fin = 9;
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                mostrarNrosPaginas(ini);
            }
        }

        private void botonPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (sePuedeRetrocederPaginas())
            {
                ini -= publicacionesPorPagina;
                if (fin != totalPublicaciones)
                {
                    fin -= publicacionesPorPagina;
                }
                else
                {
                    fin = ini + 9;
                }

                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                mostrarNrosPaginas(ini);
            }
        }

        private bool sePuedeAvanzarPaginas()
        {
            if (VerificarSiSeBusco() == false)
            {
                return false;
            }
            else
            {
                if (paginaActual == totalPaginas)
                {
                    MessageBox.Show("Ya estas en la ultima pagina");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void botonPaginaSiguiente_Click(object sender, EventArgs e)
        {
            if (sePuedeAvanzarPaginas())
            {
                ini += publicacionesPorPagina;
                if ((fin + publicacionesPorPagina) != totalPublicaciones)
                {
                    fin += publicacionesPorPagina;
                }
                else
                {
                    fin = totalPublicaciones;
                }

                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                mostrarNrosPaginas(ini);
            }
        }

        private void botonUltimaPagina_Click(object sender, EventArgs e)
        {
            if (sePuedeAvanzarPaginas())
            {
                ini = (totalPaginas - 1) * publicacionesPorPagina;
                fin = totalPublicaciones;
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                mostrarNrosPaginas(ini);
            }
        }

        private void AgregarBotonVerPublicacion()
        {
            if (dataGridView1.Columns.Contains("Ver Publicacion"))
                dataGridView1.Columns.Remove("Ver Publicacion");
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Ver Publicacion";
                buttons.Text = "Ver Publicacion";
                buttons.Name = "Ver Publicacion";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
                dataGridView1.CellClick +=
                    new DataGridViewCellEventHandler(dataGridView1_CellClick);
            }

            dataGridView1.Columns.Add(buttons);


        }        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Ver Publicacion"].Index)
            {
                int idPublicacionElegida = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                this.Hide();
                new VerPublicacion(idPublicacionElegida).ShowDialog();
                this.Close();
            }
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxDescripcion.Clear();
            comboBoxRubro.SelectedIndex = -1;
            labelNrosPagina.Text = "";
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Remove("Ver Publicacion");
        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }
                
    }
}
