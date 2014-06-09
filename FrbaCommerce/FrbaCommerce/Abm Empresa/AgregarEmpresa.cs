﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.ABM_Empresa
{
    public partial class AgregarEmpresa : Form
    {
        public AgregarEmpresa()
        {
            InitializeComponent();
        }

        private void AgregarEmpresa_Load(object sender, EventArgs e)
        {
            AgregarListenerACalendario();
        }

        private void AgregarListenerACalendario()
        {
            this.monthCalendar_FechaDeCreacion.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_FechaDeNacimiento_DateSelected);
        }

        private void button_FechaDeCreacion_Click(object sender, EventArgs e)
        {

        }
    }
}
