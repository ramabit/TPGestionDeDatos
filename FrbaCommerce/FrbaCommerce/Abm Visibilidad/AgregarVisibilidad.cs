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
    public partial class AgregarVisibilidad : Form
    {
        private ComunicadorConBaseDeDatos comunicador = new ComunicadorConBaseDeDatos();

        public AgregarVisibilidad()
        {
            InitializeComponent();
        }

        private void AgregarVisibilidad_Load(object sender, EventArgs e)
        {

        }

        private void button_Guardar_Click(object sender, EventArgs e)
        {
            String descripcion = textBox_Descripcion.Text;
            String precioPorPublicar = textBox_PrecioPorPublicar.Text;
            String porcentajePorVenta = textBox_PorcentajePorVenta.Text;
            String duracion = textBox_Duracion.Text;

            // Insert Visibilidad
            try
            {
                Visibilidad visibilidad = new Visibilidad();
                visibilidad.SetDescripcion(descripcion);
                visibilidad.SetPrecioPorPublicar(precioPorPublicar);
                visibilidad.SetPorcentajePorVenta(porcentajePorVenta);
                visibilidad.SetDuracion(duracion);
                Decimal idVisibilidad = comunicador.CrearVisibilidad(visibilidad);
                if (idVisibilidad > 0) MessageBox.Show("Se creo la visibilidad");
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
            catch (VisibilidadYaExisteException exception)
            {
                MessageBox.Show("Ya existe esa visibilidad");
                return;
            }

            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void button_Limpiar_Click(object sender, EventArgs e)
        {
            textBox_Descripcion.Text = "";
            textBox_PorcentajePorVenta.Text = "";
            textBox_PrecioPorPublicar.Text = "";
            textBox_Duracion.Text = "";
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }
    }
}
