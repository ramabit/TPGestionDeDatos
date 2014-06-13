using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Facturar_Publicaciones
{
    public partial class Facturar : Form
    {
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
        private SqlCommand command;
        private IList<SqlParameter> parametros = new List<SqlParameter>();

        public Facturar()
        {
            InitializeComponent();
        }

        private void Facturar_Load(object sender, EventArgs e)
        {
          // dropdownCalificacion.Items.Add(10);
           CargarCostosPublicacionPorFacturar();
           CargarComisionesVentasPorFacturar();
           
        }

        private void CargarCostosPublicacionPorFacturar()
        {
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));

            String cantidadCostos = "select COUNT(p.id) from LOS_SUPER_AMIGOS.Publicacion p,"
            + " LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u"
            + " where p.usuario_id = u.id and u.id = @id and p.visibilidad_id = v.id"
            + " and p.costo_pagado = 0 and p.estado = 'Finalizada'";

            int cantidad  = (int)builderDeComandos.Crear(cantidadCostos, parametros).ExecuteScalar();

            labelCantidadCostos.Text = cantidad.ToString();
        }

        private void CargarComisionesVentasPorFacturar()
        {

        }

        private void botonFacturar_Click(object sender, EventArgs e)
        {

        }

        private void labelCantCompras_Click(object sender, EventArgs e)
        {

        }
    }
}
