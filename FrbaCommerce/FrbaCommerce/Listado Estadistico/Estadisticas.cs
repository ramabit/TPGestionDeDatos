using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.Listado_Estadistico
{
    public partial class Estadisticas : Form
    {
        public Estadisticas()
        {
            InitializeComponent();
        }

        private void Estadisticas_Load(object sender, EventArgs e)
        {
            CargarTrimestres();
            CargarTiposDeListados();
        }

        private void CargarTrimestres()
        {
            DataTable trimestres = new DataTable();
            trimestres.Columns.Add("trimestres");
            trimestres.Rows.Add("1er trimestre (Enero - Marzo");
            trimestres.Rows.Add("2do trimestre (Abril - Junio");
            trimestres.Rows.Add("3er trimestre (Julio - Septiembre");
            trimestres.Rows.Add("4to trimestre (Octubre - Diciembre");
            comboBox_Trimestre.DataSource = trimestres;
            comboBox_Trimestre.ValueMember = "trimestres";
            comboBox_Trimestre.SelectedIndex = -1;
        }

        private void CargarTiposDeListados()
        {
            DataTable tiposDeListados = new DataTable();
            tiposDeListados.Columns.Add("tiposDeListados");
            tiposDeListados.Rows.Add("Vendedores con mayor cantidad de productos no vendidos");
            tiposDeListados.Rows.Add("Vendedores con mayor facturacion");
            tiposDeListados.Rows.Add("Vendedores con mayores calificaciones");
            tiposDeListados.Rows.Add("Clientes con mayor cantidad de publicaciones sin calificar");
            comboBox_TipoDeListado.DataSource = tiposDeListados;
            comboBox_TipoDeListado.ValueMember = "tiposDeListados";
            comboBox_TipoDeListado.SelectedIndex = -1;
        }

        private void button_Buscar_Click(object sender, EventArgs e)
        {
            String anio = textBox_Anio.Text;
            String trimestre = comboBox_Trimestre.Text;
            String tipoDeListado = comboBox_TipoDeListado.Text;

            String queryParaObtenerResultados = GetQueryObtenerResultados(tipoDeListado);
        }

        private string GetQueryObtenerResultados(String tipoDeListado)
        {
            switch (tipoDeListado)
            {
                case "Vendedores con mayor cantidad de productos no vendidos":
                    return "";
                case "Vendedores con mayor facturacion":
                    return "";
                case "Vendedores con mayores calificaciones":
                    return "";
                case "Clientes con mayor cantidad de publicaciones sin calificar":
                    return "";
                default:
                    return "";
            }
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Anio.Text = "";
            comboBox_Trimestre.SelectedIndex = -1;
            comboBox_TipoDeListado.SelectedIndex = -1;
            dataGridView_Estadistica.DataSource = null;
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void textBox_Anio_TextChanged(object sender, EventArgs e)
        {
            String anio = textBox_Anio.Text;
            if (esNumero(anio) && tieneCuatroNumeros(anio))
            {
                comboBox_Trimestre.Enabled = true;
                return;
            }
            comboBox_Trimestre.Enabled = false;
            comboBox_Trimestre.SelectedIndex = -1;
        }

        private Boolean esNumero(String anio)
        {
            UInt32 num;
            return UInt32.TryParse(anio, out num);  
        }

        private Boolean tieneCuatroNumeros(String anio)
        {
            return anio.Length == 4;
        }

        private void comboBox_Trimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Trimestre.SelectedIndex == -1)
            {
                comboBox_TipoDeListado.Enabled = false;
                comboBox_TipoDeListado.SelectedIndex = -1;
                return;
            }
            comboBox_TipoDeListado.Enabled = true;
        }

        private void comboBox_TipoDeListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_TipoDeListado.SelectedIndex == -1)
            {
                button_Buscar.Enabled = false;
                return;
            }                
            button_Buscar.Enabled = true;
        }
    }
}
