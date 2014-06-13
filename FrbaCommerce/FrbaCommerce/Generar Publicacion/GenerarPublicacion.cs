using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaCommerce.Exceptions;
using FrbaCommerce.Objetos;

namespace FrbaCommerce.Generar_Publicacion
{
    public partial class GenerarPublicacion : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public GenerarPublicacion()
        {
            InitializeComponent();
        }

        private void GenerarPublicacion_Load(object sender, EventArgs e)
        {
            CargarTiposDePublicacion();
            CargarEstados();
            CargarRubros();
            CargarVisibilidades();
        }

        private void CargarTiposDePublicacion()
        {
            DataTable tiposDePublicacion = new DataTable();
            tiposDePublicacion.Columns.Add("tipoDePublicacion");
            tiposDePublicacion.Rows.Add("Compra Inmediata");
            tiposDePublicacion.Rows.Add("Subasta");
            comboBox_TiposDePublicacion.DataSource = tiposDePublicacion;
            comboBox_TiposDePublicacion.ValueMember = "tipoDePublicacion";
        }

        private void CargarEstados()
        {
            DataTable estados = new DataTable();
            estados.Columns.Add("estados");
            estados.Rows.Add("Borrador");
            estados.Rows.Add("Publicada");
            comboBox_Estado.DataSource = estados;
            comboBox_Estado.ValueMember = "estados";
        }

        private void CargarRubros()
        {
            comboBox_Rubro.DataSource = comunicador.SelectDataTable("descripcion", "LOS_SUPER_AMIGOS.Rubro");
            comboBox_Rubro.ValueMember = "descripcion";
        }

        private void CargarVisibilidades()
        {
            comboBox_Visibilidad.DataSource = comunicador.SelectDataTable("descripcion", "LOS_SUPER_AMIGOS.Visibilidad");
            comboBox_Visibilidad.ValueMember = "descripcion";
        }

        private void button_generar_Click(object sender, EventArgs e)
        {
            String tipo = comboBox_TiposDePublicacion.Text;
            String estado = comboBox_Estado.Text;
            String descripcion = textBox_Descripcion.Text;
            String fechaDeInicio = textBox_FechaDeInicio.Text;
            String rubro = comboBox_Rubro.Text;
            String visibilidadDescripcion = comboBox_Visibilidad.Text;
            Boolean pregunta = radioButton_Pregunta.Checked;
            String stock = textBox_Stock.Text;
            String precio = textBox_Precio.Text;

            Decimal idRubro = (Decimal) comunicador.selectFromWhere("id", "Rubro", "descripcion", rubro);
            Decimal idVisibilidad = Convert.ToDecimal(comunicador.selectFromWhere("id", "Visibilidad", "descripcion", visibilidadDescripcion));
            Double duracion = Convert.ToDouble(comunicador.selectFromWhere("duracion", "Visibilidad", "descripcion", visibilidadDescripcion));
            String fechaDeVencimiento = Convert.ToString(Convert.ToDateTime(fechaDeInicio).AddDays(duracion));

            
            try
            {
                Publicacion publicacion = new Publicacion();
                publicacion.SetTipo(tipo);
                publicacion.SetEstado(estado);
                publicacion.SetDescripcion(descripcion);
                publicacion.SetFechaDeInicio(fechaDeInicio);
                publicacion.SetFechaDeVencimiento(fechaDeVencimiento);
                publicacion.SetStock(stock);
                publicacion.SetPrecio(precio);
                publicacion.SetIdRubro(idRubro);
                publicacion.SetIdVisibilidad(idVisibilidad);
                publicacion.SetIdUsuario(3);//Convert.ToDecimal(UsuarioSesion.usuario));
                Decimal idPublicacion = comunicador.CrearPublicacion(publicacion);
                if (idPublicacion > 0) MessageBox.Show("Se agrego la publicacion correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos");
                return;
            }
            
            VolverAlMenuPrincipal();
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            textBox_FechaDeInicio.Text = "";
            textBox_Precio.Text = "";
            textBox_Stock.Text = "";
            comboBox_Rubro.SelectedIndex = 0;
            comboBox_TiposDePublicacion.SelectedIndex = 0;
            comboBox_Estado.SelectedIndex = 0;
            comboBox_Visibilidad.SelectedIndex = 0;
            radioButton_Pregunta.Checked = true;
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            VolverAlMenuPrincipal();
        }

        private void VolverAlMenuPrincipal()
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void button_FechaDeInicio_Click(object sender, EventArgs e)
        {
            monthCalendar_FechaDeInicio.Visible = true;
        }

        private void monthCalendar_FechaDeInicio_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            textBox_FechaDeInicio.Text = e.Start.ToShortDateString();
            monthCalendar_FechaDeInicio.Visible = false;
        }

        private void comboBox_tiposDePublicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tipoSeleccionado = comboBox_TiposDePublicacion.Text;
            if (tipoSeleccionado == "Compra Inmediata")
            {
                label_stock.Text = "Stock";
                label_precio.Text = "Precio por unidad";
            }
            else
            {
                label_stock.Text = "Cantidad";
                label_precio.Text = "Precio inicial";
            }
        }
    }
}
