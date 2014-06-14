using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;
using System.Data.SqlClient;

namespace FrbaCommerce.Objetos
{
    class Visibilidad : Comunicable
    {
        private Decimal id;
        private String descripcion;
        private String precioPorPublicar;
        private String porcentajePorVenta;
        private String duracion;

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        public void SetDescripcion(String descripcion)
        {
            if (descripcion == "")
                throw new CampoVacioException();
            this.descripcion = descripcion;
        }

        public String GetDescripcion()
        {
            return this.descripcion;
        }

        public void SetPrecioPorPublicar(String precioPorPublicar)
        {
            if (precioPorPublicar == "")
                throw new CampoVacioException();
            this.precioPorPublicar = precioPorPublicar;
        }

        public String GetPrecioPorPublicar()
        {
            return this.precioPorPublicar;
        }

        public void SetPorcentajePorVenta(String porcentajePorVenta)
        {
            if (porcentajePorVenta == "")
                throw new CampoVacioException();
            this.porcentajePorVenta = porcentajePorVenta;
        }

        public String GetPorcentajePorVenta()
        {
            return this.porcentajePorVenta;
        }

        public void SetDuracion(String duracion)
        {
            if (duracion == "")
                throw new CampoVacioException();
            this.duracion = duracion;
        }

        public String GetDuracion()
        {
            return this.duracion;
        }

        #region Miembros de Comunicable

        string Comunicable.GetQueryCrear()
        {
            return "LOS_SUPER_AMIGOS.crear_visibilidad";
        }

        IList<System.Data.SqlClient.SqlParameter> Comunicable.GetParametros()
        {
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@descripcion", this.descripcion));
            parametros.Add(new SqlParameter("@precio", this.precioPorPublicar));
            parametros.Add(new SqlParameter("@porcentaje", this.porcentajePorVenta));
            parametros.Add(new SqlParameter("@duracion", this.duracion));
            return parametros;
        }

        string Comunicable.GetQueryModificar()
        {
            return "UPDATE LOS_SUPER_AMIGOS.Visibilidad SET descripcion = @descripcion, precio = @precioporPublicar, porcentaje = @porcentajeDeVenta, duracion = @duracion WHERE id = @idVisibilidad";
        }

        #endregion
    }
}
