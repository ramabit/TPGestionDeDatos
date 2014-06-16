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
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

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
            trimestres.Rows.Add("1er trimestre (Enero - Marzo)");
            trimestres.Rows.Add("2do trimestre (Abril - Junio)");
            trimestres.Rows.Add("3er trimestre (Julio - Septiembre)");
            trimestres.Rows.Add("4to trimestre (Octubre - Diciembre)");
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

            String fechaDeInicio = ObtenerFechaDeInicio(anio, trimestre);
            String fechaDeFin = ObtenerFechaDeFin(anio, trimestre);
            String queryParaObtenerResultados = GetQueryObtenerResultados(tipoDeListado, fechaDeInicio, fechaDeFin);
            
            dataGridView_Estadistica.DataSource = comunicador.SelectDataTable("*", queryParaObtenerResultados);
        }

        private String ObtenerFechaDeInicio(string anio, string trimestre)
        {
            String dia = "01";
            String mes = ObtenerMesInicio(trimestre);
            return dia + "/" + mes + "/" + anio;
        }

        private string ObtenerMesInicio(string trimestre)
        {
            switch (trimestre[0])
            {
                case '1':
                    return "01";
                case '2':
                    return "04";
                case '3':
                    return "07";
                case '4':
                    return "10";
            }
            throw new Exception("No pudo obtener mes");
        }

        private String ObtenerFechaDeFin(string anio, string trimestre)
        {
            String dia = "01";
            String mes = ObtenerMesFin(trimestre);
            return dia + "/" + mes + "/" + anio;
        }

        private string ObtenerMesFin(string trimestre)
        {
            switch (trimestre[0])
            {
                case '1':
                    return "03";
                case '2':
                    return "06";
                case '3':
                    return "09";
                case '4':
                    return "12";
            }
            throw new Exception("No pudo obtener mes");
        }

        private string GetQueryObtenerResultados(String tipoDeListado, String fechaDeInicio, String fechaDeFin)
        {
            switch (tipoDeListado)
            {
                case "Vendedores con mayor cantidad de productos no vendidos":
                    return "";
                case "Vendedores con mayor facturacion":
                    return "LOS_SUPER_AMIGOS.vendedores_con_mayor_facturacion('" + fechaDeInicio + "', '" + fechaDeFin + "')";
                case "Vendedores con mayores calificaciones":
                    return "LOS_SUPER_AMIGOS.vendedores_con_mayor_calificacion('" + fechaDeInicio + "', '" + fechaDeFin + "')";
                case "Clientes con mayor cantidad de publicaciones sin calificar":
                    return "LOS_SUPER_AMIGOS.clientes_con_publicaciones_sin_calificar('" + fechaDeInicio + "', '" + fechaDeFin + "')";
            }
            throw new Exception("No se pudo obtener la funcion");
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
