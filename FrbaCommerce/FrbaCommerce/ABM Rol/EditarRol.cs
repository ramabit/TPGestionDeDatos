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
    public partial class EditarRol : Form
    {
        public EditarRol()
        {
            InitializeComponent();
        }

        private void EditarRol_Load(object sender, EventArgs e)
        {

        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            new RolForm().Show();
            this.Close();
        }
    }
}
