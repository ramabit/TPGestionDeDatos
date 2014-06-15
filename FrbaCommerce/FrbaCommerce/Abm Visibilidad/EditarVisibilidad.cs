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

namespace FrbaCommerce.ABM_Visibilidad
{
    public partial class EditarVisibilidad : Form
    {
        private Decimal idVisibilidad;
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public EditarVisibilidad(String idVisibilidad)
        {
            InitializeComponent();
            this.idVisibilidad = Convert.ToDecimal(idVisibilidad);
        }

        private void EditarVisibilidad_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            Visibilidad visibilidad = comunicador.ObtenerVisibilidad(idVisibilidad);
            textBox_Descripcion.Text = visibilidad.GetDescripcion();
            textBox_PrecioPorPublicar.Text = visibilidad.GetPrecioPorPublicar();
            textBox_PorcentajePorVenta.Text = visibilidad.GetPorcentajePorVenta();
            textBox_Duracion.Text = visibilidad.GetDuracion();
        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            String descripcion = textBox_Descripcion.Text;
            String precioPorPublicar = textBox_PrecioPorPublicar.Text;
            String porcentajePorVenta = textBox_PorcentajePorVenta.Text;
            String duracion = textBox_Duracion.Text;

            // Update Visibilidad
            try
            {
                Visibilidad visibilidad = new Visibilidad();
                visibilidad.SetDescripcion(descripcion);
                visibilidad.SetPrecioPorPublicar(precioPorPublicar);
                visibilidad.SetPorcentajePorVenta(porcentajePorVenta);
                visibilidad.SetDuracion(duracion);
                Boolean pudoModificar = comunicador.Modificar(idVisibilidad, visibilidad);
                if (pudoModificar) MessageBox.Show("La visibilidad se modifico correctamente");
            }
            catch (CampoVacioException exception)
            {
                MessageBox.Show("Faltan completar campos");
                return;
            }
            catch (NoEsNumeroException exception)
            {
                MessageBox.Show("Datos mal ingresados");
                return;
            }
            catch (TelefonoYaExisteException exception)
            {
                MessageBox.Show("Ya existe esa descripcion");
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
