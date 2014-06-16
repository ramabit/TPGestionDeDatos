using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;
using System.Data.SqlClient;

namespace FrbaCommerce.Objetos
{
    class Empresa : Objeto, Comunicable
    {
        private Decimal id;
        private String razonSocial;
        private String nombreDeContacto;
        private String cuit;
        private DateTime fechaDeCreacion;
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

            if (!esNumero(cuit))
                throw new FormatoInvalidoException();

            this.cuit = cuit;
        }

        public String GetCuit()
        {
            return this.cuit;
        }

        public void SetFechaDeCreacion(DateTime fechaDeCreacion)
        {
            if (fechaDeCreacion.ToString() == "")
                throw new CampoVacioException();

            if (!esFecha(fechaDeCreacion.ToString()))
                throw new FormatoInvalidoException();

            this.fechaDeCreacion = fechaDeCreacion;
        }

        public DateTime GetFechaDeCreacion()
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

            if (!esNumero(telefono))
                throw new FormatoInvalidoException();

            this.telefono = telefono;
        }

        public String GetTelefono()
        {
            return this.telefono;
        }

        public void SetIdDireccion(Decimal idDireccion)
        {
            if (idDireccion == 0)
                throw new CampoVacioException();
            this.idDireccion = idDireccion;
        }

        public Decimal GetIdDireccion()
        {
            return this.idDireccion;
        }

        public void SetIdUsuario(Decimal idUsuario)
        {
            if (idUsuario == 0)
                throw new CampoVacioException();
            this.idUsuario = idUsuario;
        }

        public Decimal GetIdUsuario()
        {
            return this.idUsuario;
        }

        #region Miembros de Comunicable

        string Comunicable.GetQueryCrear()
        {
            return "LOS_SUPER_AMIGOS.crear_empresa";
        }

        string Comunicable.GetQueryModificar()
        {
            return "UPDATE LOS_SUPER_AMIGOS.Empresa SET razon_social = @razon_social, nombre_de_contacto = @nombre_de_contacto, cuit = @cuit, fecha_creacion = @fecha_creacion, mail = @mail, telefono = @telefono, ciudad = @ciudad  WHERE id = @id";
        }

        public string GetQueryObtener()
        {
            return "SELECT * FROM LOS_SUPER_AMIGOS.Empresa WHERE id = @id";
        }

        IList<System.Data.SqlClient.SqlParameter> Comunicable.GetParametros()
        {
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@razon_social", this.razonSocial));
            parametros.Add(new SqlParameter("@nombre_de_contacto", this.nombreDeContacto));
            parametros.Add(new SqlParameter("@cuit", this.cuit));
            parametros.Add(new SqlParameter("@fecha_creacion", this.fechaDeCreacion));
            parametros.Add(new SqlParameter("@mail", this.mail));
            parametros.Add(new SqlParameter("@telefono", this.telefono));
            parametros.Add(new SqlParameter("@ciudad", this.ciudad));
            parametros.Add(new SqlParameter("@direccion_id", this.idDireccion));
            parametros.Add(new SqlParameter("@usuario_id", this.idUsuario));
            return parametros;
        }

        public void CargarInformacion(SqlDataReader reader)
        {
            this.razonSocial = Convert.ToString(reader["razon_social"]);
            this.nombreDeContacto = Convert.ToString(reader["nombre_de_contacto"]);
            this.fechaDeCreacion = Convert.ToString(reader["fecha_creacion"]);
            this.cuit = Convert.ToString(reader["cuit"]);
            this.mail = Convert.ToString(reader["mail"]);
            this.telefono = Convert.ToString(reader["telefono"]);
            this.ciudad = Convert.ToString(reader["ciudad"]);
            this.idDireccion = Convert.ToDecimal(reader["direccion_id"]);
            this.idUsuario = Convert.ToDecimal(reader["usuario_id"]);
        }

        #endregion
    }
}
