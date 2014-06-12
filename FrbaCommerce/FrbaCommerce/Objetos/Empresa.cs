using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;

namespace FrbaCommerce.Objetos
{
    class Empresa
    {
        private Decimal id;
        private String razonSocial;
        private String nombreDeContacto;
        private String cuit;
        private String fechaDeCreacion;
        private String mail;
        private String telefono;
        private String ciudad;
        private Decimal idDireccion;
        private Decimal idUsuario;

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        public void SetRazonSocial(String razonSocial)
        {
            if (razonSocial == "")
                throw new CampoVacioException();
            this.razonSocial = razonSocial;
        }

        public String GetRazonSocial()
        {
            return this.razonSocial;
        }

        public void SetNombreDeContacto(String nombreDeContacto)
        {
            if (nombreDeContacto == "")
                throw new CampoVacioException();
            this.nombreDeContacto = nombreDeContacto;
        }

        public String GetNombreDeContacto()
        {
            return this.nombreDeContacto;
        }

        public void SetCuit(String cuit)
        {
            if (cuit == "")
                throw new CampoVacioException();
            this.cuit = cuit;
        }

        public String GetCuit()
        {
            return this.cuit;
        }

        public void SetFechaDeCreacion(String fechaDeCreacion)
        {
            if (fechaDeCreacion == "")
                throw new CampoVacioException();
            this.fechaDeCreacion = fechaDeCreacion;
        }

        public String GetFechaDeCreacion()
        {
            return this.fechaDeCreacion;
        }

        public void SetCiudad(String ciudad)
        {
            if (ciudad == "")
                throw new CampoVacioException();
            this.ciudad = ciudad;
        }

        public String GetCiudad()
        {
            return this.ciudad;
        }

        public void SetMail(String mail)
        {
            if (mail == "")
                throw new CampoVacioException();
            this.mail = mail;
        }

        public String GetMail()
        {
            return this.mail;
        }

        public void SetTelefono(String telefono)
        {
            if (telefono == "")
                throw new CampoVacioException();
            this.telefono = telefono;
        }

        public String GetTelefono()
        {
            return this.telefono;
        }

        public void SetIdDireccion(Decimal idDireccion)
        {
            this.idDireccion = idDireccion;
        }

        public Decimal GetIdDireccion()
        {
            return this.idDireccion;
        }

        public void SetIdUsuario(Decimal idUsuario)
        {
            this.idUsuario = idUsuario;
        }

        public Decimal GetIdUsuario()
        {
            return this.idUsuario;
        }
    }
}
