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
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        int cantidadMin;
        int cantidadMax;
        private SqlCommand command;
        private String formaDePago;

        public Facturar()
        {
            InitializeComponent();
        }

        private void Facturar_Load(object sender, EventArgs e)
        {
           CargarCostosPublicacionPorFacturar();
           CargarComisionesVentasPorFacturar();
           CalcularMonto();
           radioButtonEfectivo.Checked = true;
           textBoxBanco.Enabled = false;
           textBoxNumero.Enabled = false;
        }

        // Calculo el monto a pagar en la factura
        private void CalcularMonto()
        {
            int valor;
            Int32.TryParse(dropDownFacturar.Text, out valor);

            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            parametros.Add(new SqlParameter("@cant", valor));

            String monto = "create table LOS_SUPER_AMIGOS.Compra_Comision"
                        + " (compra_id numeric(18,0),"
                        + " compra_fecha datetime,"
                        + " compra_publicacion numeric(18,0),"
                        + " compra_cantidad numeric(18,0))"
                        + " insert into LOS_SUPER_AMIGOS.Compra_Comision"
                        + " (compra_id, compra_fecha, compra_publicacion, compra_cantidad)"
                        + " select top (@cant) c.id, c.fecha, c.publicacion_id, c.cantidad"
                        + " from LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p"
                        + " where u.id = @id and p.usuario_id = u.id and c.publicacion_id = p.id and c.facturada = 0"
                        + " order by c.fecha"
                        + " select (select  isnull( (select sum(cc.compra_cantidad * p.precio * v.porcentaje)"
                        + " from LOS_SUPER_AMIGOS.Compra_Comision cc, LOS_SUPER_AMIGOS.Compra c,"
                        + " LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Visibilidad v"
                        + " where cc.compra_id = c.id and c.publicacion_id = p.id and p.visibilidad_id = v.id),0))"
                        + " + (select isnull( (select sum(v.precio)"
                        + " from LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u"
                        + " where p.costo_pagado = 0 and p.visibilidad_id = v.id and p.usuario_id = u.id and u.id = @id"
                        + " and p.estado = 'Finalizada'),0))";
            Double montoCalculado = Convert.ToDouble(builderDeComandos.Crear(monto, parametros).ExecuteScalar());
            labelMontoCalculado.Text = montoCalculado.ToString();


            // Borro tabla temporal con monto
            String borroTablaTemporal = "drop table LOS_SUPER_AMIGOS.Compra_Comision";
            parametros.Clear();
            builderDeComandos.Crear(borroTablaTemporal, parametros).ExecuteNonQuery();
        }

        private void CargarCostosPublicacionPorFacturar()
        {
            parametros.Clear();
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
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));

            String cantidadMinimaComisiones = "select COUNT(c.id) from LOS_SUPER_AMIGOS.Usuario u,"
             + " LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p"
             + " where u.id = @id and p.usuario_id = u.id and c.publicacion_id = p.id"
             + " and c.facturada = 0 and p.estado = 'Finalizada'";

            cantidadMin = (int)builderDeComandos.Crear(cantidadMinimaComisiones, parametros).ExecuteScalar();

            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));

            String cantidadMaximaComisiones = "select COUNT(c.id) from LOS_SUPER_AMIGOS.Usuario u,"
             + " LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p"
             + " where u.id = @id and p.usuario_id = u.id and c.publicacion_id = p.id"
             + " and c.facturada = 0";

            cantidadMax = (int)builderDeComandos.Crear(cantidadMaximaComisiones, parametros).ExecuteScalar();

            dropDownFacturar.Text = cantidadMin.ToString();
            labelMinimo.Text = cantidadMin.ToString();
            labelMaximo.Text = cantidadMax.ToString();
            while (cantidadMax >= cantidadMin)
            {
                dropDownFacturar.Items.Add(cantidadMax);
                cantidadMax--;
            }
        }

        private void botonFacturar_Click(object sender, EventArgs e)
        {
          
            // Creo la nueva factura
            String creoFactura = "insert LOS_SUPER_AMIGOS.Factura"
                                + "(fecha) values(GETDATE())";
            parametros.Clear();
            builderDeComandos.Crear(creoFactura, parametros).ExecuteNonQuery();

            // Obtengo el id de la nueva factura
            String idFactura = "select top 1 f.nro from LOS_SUPER_AMIGOS.Factura f order by f.nro DESC";
            parametros.Clear();
            Decimal idFact = (Decimal)builderDeComandos.Crear(idFactura,parametros).ExecuteScalar();

            // Inserto los items factura de costos de publicaciones finalizadas
            String costo_publicacion = "insert LOS_SUPER_AMIGOS.Item_Factura"
                        + " (cantidad, factura_nro, monto, publicacion_id)"
                        + " select 1, @idF, v.precio, p.id"
                        + " from LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u"
                        + " where p.costo_pagado = 0 and p.visibilidad_id = v.id and p.usuario_id = u.id and u.id = @id"
                        + " and p.estado = 'Finalizada'";
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            parametros.Add(new SqlParameter("@idF", idFact));
            builderDeComandos.Crear(costo_publicacion, parametros).ExecuteNonQuery();

            // Actualizo campo costo pagado
            String costoPagado = "declare @pid numeric(18,0)"
                            + " declare publ_cursor cursor for"
                            + " (select p.id from LOS_SUPER_AMIGOS.Publicacion p,"
                            + " LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u"
                            + " where p.costo_pagado = 0 and p.visibilidad_id = v.id and p.usuario_id = u.id"
                            + " and u.id = 72 and p.estado = 'Finalizada')"
                            + " open publ_cursor"
                            + " fetch next from publ_cursor into @pid"
                            + " while @@FETCH_STATUS = 0"
                            + " Begin "
                            + " update LOS_SUPER_AMIGOS.Publicacion"
                            + " set costo_pagado = 1"
                            + " where id = @pid"
                            + " fetch next from publ_cursor into @pid"
                            + " End"
                            + " close publ_cursor"
                            + " deallocate publ_cursor";
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            builderDeComandos.Crear(costoPagado, parametros).ExecuteNonQuery();

            int valor;
            Int32.TryParse(dropDownFacturar.Text, out valor);
            
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            parametros.Add(new SqlParameter("@cant", valor));
            
            // Creo una tabla con todas las ventas por facturar
            String totalidadVentasFacturar = "create table LOS_SUPER_AMIGOS.Compra_Comision"
                                    + " (compra_id numeric(18,0),"
                                    + " compra_fecha datetime,"
                                    + " compra_publicacion numeric(18,0),"
                                    + " compra_cantidad numeric(18,0))"
                                    + " insert into LOS_SUPER_AMIGOS.Compra_Comision"
                                    + " (compra_id, compra_fecha, compra_publicacion, compra_cantidad)" 
                                    + " select top (@cant) c.id, c.fecha, c.publicacion_id, c.cantidad"
                                    + " from LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p"
                                    + " where u.id = @id and p.usuario_id = u.id and c.publicacion_id = p.id and c.facturada = 0"
                                    + " order by c.fecha";
            builderDeComandos.Crear(totalidadVentasFacturar, parametros).ExecuteNonQuery();


            String consulta = "LOS_SUPER_AMIGOS.SacarBonificaciones";
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            SqlParameter parametroContador = new SqlParameter("@id", SqlDbType.Int);
            parametroContador.Direction = ParameterDirection.Output;
            parametros.Add(parametroContador);
            SqlParameter parametroMonto = new SqlParameter("@id", SqlDbType.Decimal);
            parametroMonto.Direction = ParameterDirection.Output;
            parametros.Add(parametroMonto);
            command = builderDeComandos.Crear(consulta, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();

            int cantidadDeBonificaciones = (int)parametroContador.Value;
            Decimal montoDescontadoBonificaciones = 0;
            montoDescontadoBonificaciones = (Decimal)parametroMonto.Value;

            String borroTablaBon = "drop table LOS_SUPER_AMIGOS.Compra_Comision";
            parametros.Clear();
            builderDeComandos.Crear(borroTablaBon, parametros).ExecuteNonQuery();


            parametros.Clear();
            parametros.Add(new SqlParameter("@id", UsuarioSesion.Usuario.id));
            parametros.Add(new SqlParameter("@cant", valor - cantidadDeBonificaciones));
            // Obtengo las ventas que voy a facturar pagando su comision
            String obtengoComprasPorComisionar = "create table LOS_SUPER_AMIGOS.Compra_Comision"
                                            + " (compra_id numeric(18,0),"
                                             + " compra_fecha datetime,"
                                             + " compra_publicacion numeric(18,0),"
                                             + " compra_cantidad numeric(18,0))"
                        + " insert into LOS_SUPER_AMIGOS.Compra_Comision"
                        + " (compra_id, compra_fecha, compra_publicacion, compra_cantidad)"
                         + " select top (@cant) c.id, c.fecha, c.publicacion_id, c.cantidad"
                        + " from LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p"
                        + " where u.id = @id and p.usuario_id = u.id and c.publicacion_id = p.id and c.facturada = 0"
                        + " order by c.fecha";
            builderDeComandos.Crear(obtengoComprasPorComisionar,parametros).ExecuteNonQuery();


            // Creo los items factura de las comisiones por ventas
            parametros.Clear();
            parametros.Add(new SqlParameter("@idF", idFact));
            String creoItemsFactura = "insert LOS_SUPER_AMIGOS.Item_Factura"
                        + " (factura_nro, publicacion_id, cantidad, monto)"
                        + " (select  @idF, cc.compra_publicacion, cc.compra_cantidad, cc.compra_cantidad * p.precio * v.porcentaje"
                        + " from LOS_SUPER_AMIGOS.Compra_Comision cc, LOS_SUPER_AMIGOS.Compra c,"
                        + " LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Visibilidad v"
                        + " where cc.compra_id = c.id and c.publicacion_id = p.id and p.visibilidad_id = v.id)";
            builderDeComandos.Crear(creoItemsFactura, parametros).ExecuteNonQuery();

            // Actualizo el campo facturada en las compras que facturo
            String ventaFacturada = "declare @cid numeric(18,0)"
                                + " declare compra_cursor cursor for"
                                + " (select c.id from LOS_SUPER_AMIGOS.Compra_Comision cc,"
                                + " LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p,"
                                + " LOS_SUPER_AMIGOS.Visibilidad v"
                                + " where cc.compra_id = c.id and c.publicacion_id = p.id and p.visibilidad_id = v.id)"
                                + " open compra_cursor"
                                + " fetch next from compra_cursor into @cid"
                                + " while @@FETCH_STATUS = 0"
                                + " Begin"
                                + " update LOS_SUPER_AMIGOS.Compra"
                                + " set facturada = 1"
                                + " where id = @cid"
                                + " fetch next from compra_cursor into @cid"
                                + " End"
                                + " close compra_cursor"
                                + " deallocate compra_cursor";
            parametros.Clear();
            builderDeComandos.Crear(ventaFacturada,parametros).ExecuteNonQuery();

            // Inserto el total en la factura
            String actualizoTotal = "update LOS_SUPER_AMIGOS.Factura"
                               + " set total = (select SUM(i.monto)"
                               + " from LOS_SUPER_AMIGOS.Item_Factura i"
                               + " where i.factura_nro = nro) where nro = @idF";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idF", idFact));
            builderDeComandos.Crear(actualizoTotal, parametros).ExecuteNonQuery();

            // Inserto la forma de pago en la factura
            String formaPago = "update LOS_SUPER_AMIGOS.Factura"
                        + " set forma_pago_id = (select f.id from LOS_SUPER_AMIGOS.Forma_Pago f where f.descripcion = @pago)"
                        + " where id = @idF";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idF", idFact));
            parametros.Add(new SqlParameter("@pago", formaDePago));
            builderDeComandos.Crear(formaPago, parametros).ExecuteNonQuery();

            // Borro tabla temporal con ventas que se facturan pagando comision
            String borroTablaTemporal = "drop table LOS_SUPER_AMIGOS.Compra_Comision";
            parametros.Clear();
            builderDeComandos.Crear(borroTablaTemporal, parametros).ExecuteNonQuery();

            if (labelCantidadCostos.Text != "0" || dropDownFacturar.Text != "0")
            {
                MessageBox.Show("Factura realizada. Por bonificaciones se desconto: " + montoDescontadoBonificaciones);
            }
            else
            {
                MessageBox.Show("No hay nada para facturar");
            }
            CargarCostosPublicacionPorFacturar();
            CargarComisionesVentasPorFacturar();
            CalcularMonto();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuPrincipal().ShowDialog();
            this.Close();
        }

        private void radioButtonTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTarjeta.Checked)
            {
                textBoxNumero.Enabled = true;
                textBoxBanco.Enabled = true;
                formaDePago = "Tarjeta de credito";
            }
            else
            {
                textBoxNumero.Enabled = false;
                textBoxBanco.Enabled = false;
            }
        }

        private void radioButtonEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEfectivo.Checked)
            {
                formaDePago = "Efectivo";
            }
        }

        private void dropDownFacturar_SelectedItemChanged(object sender, EventArgs e)
        {
            CalcularMonto();
        }

    }
}
