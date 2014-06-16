using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaCommerce.Objetos;
using FrbaCommerce.Exceptions;

namespace FrbaCommerce.Editar_Publicacion
{
    public partial class EditarPublicacion : Form
    {
        private Decimal idPublicacion;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public EditarPublicacion(String idPublicacion)
        {
            InitializeComponent();
            this.idPublicacion = Convert.ToDecimal(idPublicacion);
        }

        private void EditarPublicacion_Load(object sender, EventArgs e)
        {
            CargarTiposDePublicacion();
            CargarEstados();
            CargarRubros();
            CargarVisibilidades();
            CargarDatos();
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

            String estado = (String) comunicador.SelectFromWhere("estado", "Publicacion", "id", idPublicacion);

            if (estado == "Borrador") CargarSegunBorrador(estados);
            if (estado == "Publicada") CargarSegunPublicada(estados);
            if (estado == "Pausada") CargarSegunPausada(estados);
            if (estado == "Finalizada") CargarSegunFinalizada(estados);

            comboBox_Estado.DataSource = estados;
            comboBox_Estado.ValueMember = "estados";
        }

        private void CargarSegunBorrador(DataTable estados)
        {
            estados.Rows.Add("Borrador");
            estados.Rows.Add("Publicada");
        }

        private void CargarSegunPublicada(DataTable estados)
        {
            estados.Rows.Add("Publicada");
            estados.Rows.Add("Pausada");
            estados.Rows.Add("Finalizada");
            DesactivarCamposDeCaracteristicasComunes();
            DesactivarCamposDeCaracteristicasEspeciales();
            textBox_Descripcion.Enabled = true;
            textBox_Stock.Enabled = true;
        }

        private void CargarSegunPausada(DataTable estados)
        {
            estados.Rows.Add("Publicada");
            estados.Rows.Add("Pausada");
            estados.Rows.Add("Finalizada");
            DesactivarCamposDeCaracteristicasComunes();
            DesactivarCamposDeCaracteristicasEspeciales();
        }

        private void CargarSegunFinalizada(DataTable estados)
        {
            estados.Rows.Add("Finalizada");
            comboBox_Estado.Enabled = false;
            DesactivarCamposDeCaracteristicasComunes();
            DesactivarCamposDeCaracteristicasEspeciales();
        }

        private void DesactivarCamposDeCaracteristicasComunes()
        {
            textBox_Descripcion.Enabled = false;
            textBox_FechaDeInicio.Enabled = false;
            button_FechaDeInicio.Enabled = false;
            comboBox_Rubro.Enabled = false;
            comboBox_Visibilidad.Enabled = false;
            checkBox_Pregunta.Enabled = false;
        }

        private void DesactivarCamposDeCaracteristicasEspeciales()
        {
            textBox_Precio.Enabled = false;
            textBox_Stock.Enabled = false;
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

        private void CargarDatos()
        {
            Publicacion publicacion = comunicador.ObtenerPublicacion(idPublicacion);
            textBox_Descripcion.Text = publicacion.GetDescripcion();
            textBox_FechaDeInicio.Text = Convert.ToString(publicacion.GetFechaDeInicio());
            textBox_Precio.Text = publicacion.GetPrecio();
            textBox_Stock.Text = publicacion.GetStock();
            comboBox_Rubro.SelectedValue = (String) comunicador.SelectFromWhere("descripcion", "Rubro", "id", publicacion.GetIdRubro());
            comboBox_Visibilidad.SelectedValue = (String) comunicador.SelectFromWhere("descripcion", "Visibilidad", "id", publicacion.GetIdVisibilidad()); ;
            comboBox_TiposDePublicacion.SelectedValue = publicacion.GetTipo();
            comboBox_Estado.SelectedValue = publicacion.GetEstado();
            checkBox_Pregunta.Checked = Convert.ToBoolean(comunicador.SelectFromWhere("se_realizan_preguntas", "Publicacion", "id", idPublicacion));
            checkBox_Habilitado.Checked = Convert.ToBoolean(comunicador.SelectFromWhere("habilitado", "Publicacion", "id", idPublicacion));
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            String tipo = comboBox_TiposDePublicacion.Text;
            String estado = comboBox_Estado.Text;
            String descripcion = textBox_Descripcion.Text;
            DateTime fechaDeInicio = Convert.ToDateTime(textBox_FechaDeInicio.Text);
            String rubro = comboBox_Rubro.Text;
            String visibilidad = comboBox_Visibilidad.Text;
            Boolean pregunta = checkBox_Pregunta.Checked;
            String stock = textBox_Stock.Text;
            String precio = textBox_Precio.Text;

            Decimal idRubro = (Decimal) comunicador.SelectFromWhere("id", "Rubro", "descripcion", rubro);
            Decimal idVisibilidad = (Decimal)comunicador.SelectFromWhere("id", "Visibilidad", "descripcion", visibilidad);
            Double duracion = Convert.ToDouble(comunicador.SelectFromWhere("duracion", "Visibilidad", "id", idVisibilidad));

            // Update Publicacion
            try
            {
                Publicacion publicacion = new Publicacion();
                publicacion.SetTipo(tipo);
                publicacion.SetEstado(estado);
                publicacion.SetDescripcion(descripcion);
                publicacion.SetFechaDeInicio(fechaDeInicio);
                publicacion.SetFechaDeVencimiento(Convert.ToDateTime(Convert.ToString(Convert.ToDateTime(fechaDeInicio).AddDays(duracion))));
                publicacion.SetStock(stock);
                publicacion.SetPrecio(precio);
                publicacion.SetIdRubro(idRubro);
                publicacion.SetIdVisibilidad(idVisibilidad);
                Boolean pudoModificar = comunicador.Modificar(idPublicacion, publicacion);
                if (pudoModificar) MessageBox.Show("La publicacion se modifico correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos");
                return;
            }
            catch (FormatoInvalidoException exception)
            {
                MessageBox.Show("Datos mal ingresados");
                return;
            }
            catch (FechaPasadaException exception)
            {
                MessageBox.Show("Fecha no valida");
                return;
            }

            this.Close();
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
