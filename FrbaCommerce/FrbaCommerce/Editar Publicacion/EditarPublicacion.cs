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
        private String estadoInicial;
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

            estadoInicial = (String) comunicador.SelectFromWhere("estado", "Publicacion", "id", idPublicacion);
            

            if (estadoInicial == "Borrador") CargarSegunBorrador(estados);
            if (estadoInicial == "Publicada") CargarSegunPublicada(estados);
            if (estadoInicial == "Pausada") CargarSegunPausada(estados);
            if (estadoInicial == "Finalizada") CargarSegunFinalizada(estados);

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
            Decimal idTipoDePublicacion = (Decimal)comunicador.SelectFromWhere("id", "TipoDePublicacion", "descripcion", tipo);
            String estado = comboBox_Estado.Text;
            Decimal idEstado = (Decimal)comunicador.SelectFromWhere("id", "Estado", "descripcion", estado);
            String descripcion = textBox_Descripcion.Text;
            String rubro = comboBox_Rubro.Text;
            String visibilidad = comboBox_Visibilidad.Text;
            Boolean pregunta = checkBox_Pregunta.Checked;
            String stock = textBox_Stock.Text;
            String precio = textBox_Precio.Text;
            Decimal idRubro = (Decimal) comunicador.SelectFromWhere("id", "Rubro", "descripcion", rubro);
            Decimal idVisibilidad = (Decimal)comunicador.SelectFromWhere("id", "Visibilidad", "descripcion", visibilidad);
            Double duracion = Convert.ToDouble(comunicador.SelectFromWhere("duracion", "Visibilidad", "id", idVisibilidad));
            Boolean habilitado = checkBox_Habilitado.Checked;
            DateTime fechaDeInicio;
            DateTime fechaDeVencimiento;

            if (estadoInicial == "Borrador")
            {
                fechaDeInicio = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DateKey"]);
                fechaDeVencimiento = Convert.ToDateTime(Convert.ToString(Convert.ToDateTime(fechaDeInicio).AddDays(duracion)));
            }
            else
            {
                fechaDeInicio = Convert.ToDateTime(comunicador.SelectFromWhere("fecha_inicio", "Publicacion", "id", idPublicacion));
                fechaDeVencimiento = Convert.ToDateTime(comunicador.SelectFromWhere("fecha_vencimiento", "Publicacion", "id", idPublicacion));
            }

            // Update Publicacion
            try
            {
                Publicacion publicacion = new Publicacion();
                publicacion.SetTipo(idTipoDePublicacion);
                publicacion.SetEstado(idEstado);
                publicacion.SetDescripcion(descripcion);
                publicacion.SetFechaDeInicio(fechaDeInicio);
                publicacion.SetFechaDeVencimiento(fechaDeVencimiento);
                publicacion.SetPregunta(pregunta);
                publicacion.SetStock(stock);
                publicacion.SetPrecio(precio);
                publicacion.SetIdRubro(idRubro);
                publicacion.SetIdVisibilidad(idVisibilidad);
                publicacion.SetHabilitado(habilitado);
                Boolean pudoModificar = comunicador.Modificar(idPublicacion, publicacion);
                if (pudoModificar) MessageBox.Show("La publicacion se modifico correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Falta completar campo: " + exception.Message);
                return;
            }
            catch (FormatoInvalidoException exception)
            {
                MessageBox.Show("Datos mal ingresados en: " + exception.Message);
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
