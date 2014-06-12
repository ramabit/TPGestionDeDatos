using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;

namespace FrbaCommerce.Objetos
{
    class Cliente
    {
        private Decimal id;
        private String nombre;
        private String apellido;
        private Decimal idTipoDeDocumento;
        private String numeroDeDocumento;
        private String fechaDeNacimiento;
        private String mail;
        private String telefono;
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

        public void SetNombre(String nombre)
        {
            if (nombre == "")
                throw new CampoVacioException();
            this.nombre = nombre;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public void SetApellido(String apellido)
        {
            if (apellido == "")
                throw new CampoVacioException();
            this.apellido = apellido;
        }

        public String GetApellido()
        {
            return this.apellido;
        }

        public void SetIdTipoDeDocumento(Decimal idTipoDeDocumento)
        {
            this.idTipoDeDocumento = idTipoDeDocumento;
        }

        public Decimal GetIdTipoDeDocumento()
        {
            return this.idTipoDeDocumento;
        }

        public void SetNumeroDeDocumento(String numeroDeDocumento)
        {
            if (numeroDeDocumento == "")
                throw new CampoVacioException();
            this.numeroDeDocumento = numeroDeDocumento;
        }

        public String GetNumeroDeDocumento()
        {
            return this.numeroDeDocumento;
        }

        public void SetFechaDeNacimiento(String fechaDeNacimiento)
        {
            if (fechaDeNacimiento == "")
                throw new CampoVacioException();
            this.fechaDeNacimiento = fechaDeNacimiento;
        }

        public String GetFechaDeNacimiento()
        {
            return this.fechaDeNacimiento;
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
