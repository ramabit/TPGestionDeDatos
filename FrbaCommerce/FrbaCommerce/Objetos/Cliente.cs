using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;
using System.Data.SqlClient;

namespace FrbaCommerce.Objetos
{
    class Cliente : Objeto, Comunicable
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

            if (!esNumero(numeroDeDocumento))
                throw new FormatoInvalidoException();

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

            if (!esFecha(fechaDeNacimiento))
                throw new FormatoInvalidoException();

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
            return "LOS_SUPER_AMIGOS.crear_cliente";
        }

        string Comunicable.GetQueryModificar()
        {
            return "UPDATE LOS_SUPER_AMIGOS.Cliente SET nombre = @nombre, apellido = @apellido, tipo_de_documento_id = @tipo_de_documento_id, documento = @documento, fecha_nacimiento = @fecha_nacimiento, mail = @mail, telefono = @telefono WHERE id = @id";
        }

        string Comunicable.GetQueryObtener()
        {
            return "SELECT * FROM LOS_SUPER_AMIGOS.Cliente WHERE id = @id";
        }

        IList<System.Data.SqlClient.SqlParameter> Comunicable.GetParametros()
        {
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", this.nombre));
            parametros.Add(new SqlParameter("@apellido", this.apellido));
            parametros.Add(new SqlParameter("@tipo_de_documento_id", this.idTipoDeDocumento));
            parametros.Add(new SqlParameter("@documento", this.numeroDeDocumento));
            parametros.Add(new SqlParameter("@fecha_nacimiento", this.fechaDeNacimiento));
            parametros.Add(new SqlParameter("@mail", this.mail));
            parametros.Add(new SqlParameter("@telefono", this.telefono));
            parametros.Add(new SqlParameter("@direccion_id", this.idDireccion));
            parametros.Add(new SqlParameter("@usuario_id", this.idUsuario));
            return parametros;
        }

        void Comunicable.CargarInformacion(SqlDataReader reader)
        {
            this.nombre = Convert.ToString(reader["nombre"]);
            this.apellido = Convert.ToString(reader["apellido"]);
            this.fechaDeNacimiento = Convert.ToString(reader["fecha_nacimiento"]);
            this.mail = Convert.ToString(reader["mail"]);
            this.telefono = Convert.ToString(reader["telefono"]);
            this.idTipoDeDocumento = Convert.ToDecimal(reader["tipo_de_documento_id"]);
            this.numeroDeDocumento = Convert.ToString(reader["documento"]);
            this.idDireccion = Convert.ToDecimal(reader["direccion_id"]);
            this.idUsuario = Convert.ToDecimal(reader["usuario_id"]);
        }

        #endregion
    }
}
