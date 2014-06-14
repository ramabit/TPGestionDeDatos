using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FrbaCommerce.Exceptions;
using FrbaCommerce.Objetos;

namespace FrbaCommerce
{
    class Direccion : Comunicable
    {
        private Decimal id;
        private String calle;
        private String numero;
        private String piso;
        private String departamento;
        private String codigoPostal;
        private String localidad;

        public void SetCalle(String calle)
        {
            if (calle == "")
                throw new CampoVacioException();
            this.calle = calle;
        }

        public void SetNumero(String numero)
        {
            if (numero == "")
                throw new CampoVacioException();
            this.numero = numero;
        }

        public void SetPiso(String piso)
        {
            if (piso == "")
                throw new CampoVacioException();
            this.piso = piso;
        }

        public void SetDepartamento(String departamento)
        {
            if (departamento == "")
                throw new CampoVacioException();
            this.departamento = departamento;
        }

        public void SetCodigoPostal(String codigoPostal)
        {
            if (codigoPostal == "")
                throw new CampoVacioException();
            this.codigoPostal = codigoPostal;
        }

        public void SetLocalidad(String localidad)
        {
            this.localidad = localidad;
        }

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public String GetCalle()
        {
            return this.calle;
        }

        public String GetNumero()
        {
            return this.numero;
        }

        public String GetPiso()
        {
            return this.piso;
        }

        public String GetDepartamento()
        {
            return this.departamento;
        }

        public String GetCodigoPostal()
        {
            return this.codigoPostal;
        }

        public String GetLocalidad()
        {
            return this.localidad;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        #region Miembros de Comunicable

        string Comunicable.GetQueryCrear()
        {
            return "LOS_SUPER_AMIGOS.crear_direccion";
        }

        IList<SqlParameter> Comunicable.GetParametros()
        {
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@calle", this.calle));
            parametros.Add(new SqlParameter("@numero", this.numero));
            parametros.Add(new SqlParameter("@piso", this.piso));
            parametros.Add(new SqlParameter("@depto", this.departamento));
            parametros.Add(new SqlParameter("@cod_postal", this.codigoPostal));
            parametros.Add(new SqlParameter("@localidad", this.localidad));
            return parametros;
        }

        string Comunicable.GetQueryModificar()
        {
            return "UPDATE LOS_SUPER_AMIGOS.Direccion SET calle = @calle, numero = @numero, piso = @piso, depto = @departamento, cod_postal = @codigoPostal, localidad = @localidad WHERE id = @idDireccion";
        }

        string Comunicable.GetQueryObtener()
        {
            return "SELECT * FROM LOS_SUPER_AMIGOS.Direccion WHERE id = @id";
        }

        void Comunicable.CargarInformacion(SqlDataReader reader)
        {
            this.calle = Convert.ToString(reader["calle"]);
            this.numero = Convert.ToString(reader["numero"]);
            this.piso = Convert.ToString(reader["piso"]);
            this.departamento = Convert.ToString(reader["depto"]);
            this.codigoPostal = Convert.ToString(reader["cod_postal"]);
            this.localidad = Convert.ToString(reader["localidad"]);
        }

        #endregion
    }
}
