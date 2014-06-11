using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.ABM_Rol
{
    public partial class RolForm : Form
    {
        public RolForm()
        {
            InitializeComponent();
        }

        private void botonAgregarRol_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AgregarRol().Show();
        }

        private void botonEditarRol_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ListadoEditarRol().Show();            
        }

        private void botonBajaRol_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BajaRol().Show();
        }

        private void RolForm_Load(object sender, EventArgs e)
        {

        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Close();
        }
    }
}
