using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;

namespace FrbaCommerce.Objetos
{
    class Publicacion
    {
        private Decimal id;
        private String tipo;
        private String estado;
        private String descripcion;
        private String fechaDeInicio;
        private String fechaDeVencimiento;
        private Decimal idRubro;
        private Decimal idVisibilidad;
        private Decimal idUsuario;
        private String stock;
        private String precio;

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        public void SetTipo(String tipo)
        {
            if (tipo == "")
                throw new CampoVacioException();
            this.tipo = tipo;
        }

        public String GetTipo()
        {
            return this.tipo;
        }

        public void SetEstado(String estado)
        {
            if (estado == "")
                throw new CampoVacioException();
            this.estado = estado;
        }

        public String GetEstado()
        {
            return this.estado;
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

        public void SetFechaDeInicio(String fechaDeInicio)
        {
            if (fechaDeInicio == "")
                throw new CampoVacioException();
            this.fechaDeInicio = fechaDeInicio;
        }

        public String GetFechaDeInicio()
        {
            return this.fechaDeInicio;
        }

        public void SetFechaDeVencimiento(String fechaDeVencimiento)
        {
            if (fechaDeVencimiento == "")
                throw new CampoVacioException();
            this.fechaDeVencimiento = fechaDeVencimiento;
        }

        public String GetFechaDeVencimiento()
        {
            return this.fechaDeVencimiento;
        }

        public void SetIdRubro(Decimal idRubro)
        {
            this.idRubro = idRubro;
        }

        public Decimal GetIdRubro()
        {
            return this.idRubro;
        }

        public void SetIdUsuario(Decimal idUsuario)
        {
            this.idUsuario = idUsuario;
        }

        public Decimal GetIdUsuario()
        {
            return this.idUsuario;
        }

        public void SetIdVisibilidad(Decimal idVisibilidad)
        {
            this.idVisibilidad = idVisibilidad;
        }

        public Decimal GetIdVisibilidad()
        {
            return this.idVisibilidad;
        }

        public void SetStock(String stock)
        {
            if (stock == "")
                throw new CampoVacioException();
            this.stock = stock;
        }

        public String GetStock()
        {
            return this.stock;
        }

        public void SetPrecio(String precio)
        {
            if (precio == "")
                throw new CampoVacioException();
            this.precio = precio;
        }

        public String GetPrecio()
        {
            return this.precio;
        }

    }
}
