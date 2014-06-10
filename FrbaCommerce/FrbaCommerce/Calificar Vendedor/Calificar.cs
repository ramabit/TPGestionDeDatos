using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.Calificar_Vendedor
{
    public partial class Calificar : Form
    {
        private Decimal id;
        private String tipo;
        private int calificacion = 0;
        private string descripcion = "";

        public Calificar(Decimal idCompraParaCalificar, String tipoCompraParaCalificar)
        {
            InitializeComponent();
            id = idCompraParaCalificar;
            tipo = tipoCompraParaCalificar;
        }

        private void Calificar_Load(object sender, EventArgs e)
        {
            dropdownCalificacion.Items.Add(10);
            dropdownCalificacion.Items.Add(9);
            dropdownCalificacion.Items.Add(8);
            dropdownCalificacion.Items.Add(7);
            dropdownCalificacion.Items.Add(6);
            dropdownCalificacion.Items.Add(5);
            dropdownCalificacion.Items.Add(4);
            dropdownCalificacion.Items.Add(3);
            dropdownCalificacion.Items.Add(2);
            dropdownCalificacion.Items.Add(1);

            checkBoxPredeterminado.Checked = true;

            comboBoxDescripciones.Items.Add("Vendedor muy confiable");
            comboBoxDescripciones.Items.Add("Cumplio con lo prometido");
            comboBoxDescripciones.Items.Add("No estoy completamente satisfecho con el vendedor");
            comboBoxDescripciones.Items.Add("No le compren cosas a esta persona");
        }

        private void checkBoxPredeterminado_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPredeterminado.Checked)
            {
                comboBoxDescripciones.Enabled = true;
                textBoxDescripcion.Enabled = false;
            }
            else 
            {
                comboBoxDescripciones.Enabled = false;
                textBoxDescripcion.Enabled = true;
            }
        }

        private void botonCalificar_Click(object sender, EventArgs e)
        {
            if (checkBoxPredeterminado.Checked)
            {
                if(comboBoxDescripciones.SelectedItem != null)
                {
                    descripcion = comboBoxDescripciones.SelectedItem.ToString();
                } 
            }
            else
            {
                descripcion = textBoxDescripcion.Text;
            }

            if(calificacion==0)
            {
                MessageBox.Show("Seleccione una calificacion");
                return;
            }

            MessageBox.Show("Calificacion: " + calificacion + " Descripcion: " + descripcion);

        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Listado().ShowDialog();
            this.Close();
        }

        private void dropdownCalificacion_SelectedItemChanged(object sender, EventArgs e)
        {
            calificacion = (int)dropdownCalificacion.SelectedItem;
        }
        
    }
}
