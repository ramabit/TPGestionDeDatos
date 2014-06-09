﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Generar_Publicacion
{
    public partial class GenerarPublicacion : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private String query;
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

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
            AgregarListenerACalendario();
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
            estados.Rows.Add("Activa");
            comboBox_Estado.DataSource = estados;
            comboBox_Estado.ValueMember = "estados";
        }

        private void CargarRubros()
        {
            command = builderDeComandos.Crear("SELECT descripcion FROM LOS_SUPER_AMIGOS.Rubro", parametros);

            DataSet rubros = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(rubros);
            comboBox_Rubro.DataSource = rubros.Tables[0].DefaultView;
            comboBox_Rubro.ValueMember = "descripcion";
        }

        private void CargarVisibilidades()
        {
            command = builderDeComandos.Crear("SELECT descripcion FROM LOS_SUPER_AMIGOS.Visibilidad", parametros);

            DataSet visibilidades = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(visibilidades);
            comboBox_Visibilidad.DataSource = visibilidades.Tables[0].DefaultView;
            comboBox_Visibilidad.ValueMember = "descripcion";
        }

        private void AgregarListenerACalendario()
        {
            this.monthCalendar_FechaDeInicio.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_FechaDeInicio_DateSelected);
        }

        private void button_generar_Click(object sender, EventArgs e)
        {
            String tipoSeleccionado = comboBox_TiposDePublicacion.Text;
            String estado = comboBox_Estado.Text;
            String descripcionSeleccionado = textBox_Descripcion.Text;
            DateTime fechaDeInicio = Convert.ToDateTime(textBox_FechaDeInicio.Text);
            String rubroSeleccionado = comboBox_Rubro.Text;
            String visibilidadSeleccionado = comboBox_Visibilidad.Text;
            Boolean preguntaSeleccionado = radioButton_Pregunta.Checked;
            Decimal stockSeleccionado = Convert.ToDecimal(textBox_Stock.Text);
            Double precioSeleccionado = Convert.ToDouble(textBox_Precio.Text);

            query = "SELECT id FROM LOS_SUPER_AMIGOS.Rubro WHERE descripcion = @rubroSeleccionado";
            parametros.Clear();
            parametros.Add(new SqlParameter("@rubroSeleccionado", rubroSeleccionado));
            Decimal idRubroSeleccionado = (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();

            query = "SELECT id FROM LOS_SUPER_AMIGOS.Visibilidad WHERE descripcion = @visibilidadSeleccionado";
            parametros.Clear();
            parametros.Add(new SqlParameter("@visibilidadSeleccionado", visibilidadSeleccionado));
            Decimal idVisibilidadSeleccionado = (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();

            query = "INSERT INTO LOS_SUPER_AMIGOS.Publicacion (tipo, estado, descripcion, fecha_inicio, fecha_vencimiento, rubro_id, visibilidad_id, precio, stock, usuario_id) values (@tipo, @estado, @descripcion, @fechaInicial, @fechaVencimiento, @rubroId, @visibilidadId, @precio, @stock, @usuarioId)";

            parametros.Clear();
            parametros.Add(new SqlParameter("@estado", estado));
            parametros.Add(new SqlParameter("@descripcion", descripcionSeleccionado));
            parametros.Add(new SqlParameter("@stock", stockSeleccionado));
            parametros.Add(new SqlParameter("@fechaInicial", fechaDeInicio));
            parametros.Add(new SqlParameter("@fechaVencimiento", fechaDeInicio)); // OJO ACA
            parametros.Add(new SqlParameter("@precio", precioSeleccionado));
            parametros.Add(new SqlParameter("@rubroId", idRubroSeleccionado));
            parametros.Add(new SqlParameter("@visibilidadId", idVisibilidadSeleccionado));
            parametros.Add(new SqlParameter("@usuarioId", 1));//UsuarioSesion.usuario));
            parametros.Add(new SqlParameter("@tipo", tipoSeleccionado));
            
            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();
            
            if (filasAfectadas == 1) MessageBox.Show("Se agrego la nueva publicacion correctamente");
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
