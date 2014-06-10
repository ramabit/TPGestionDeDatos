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
        private int calificacion;
        private String descripcion;

        public Calificar(Decimal idCompraParaCalificar, String tipoCompraParaCalificar)
        {
            InitializeComponent();
            id = idCompraParaCalificar;
            tipo = tipoCompraParaCalificar;
        }

        private void Calificar_Load(object sender, EventArgs e)
        {
         //   var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
         //   calificacion = Convert.ToInt16(checkedButton.Text);
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
                descripcion = comboBoxDescripciones.SelectedValue.ToString();
            }
            else
            {
                descripcion = textBoxDescripcion.Text;
            }
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Listado().ShowDialog();
            this.Close();
        }
        
    }
}
