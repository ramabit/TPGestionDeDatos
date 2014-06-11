using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace FrbaCommerce
{
    class Direccion
    {
        private String calle = "";
        private String numero = "";
        private String piso = "";
        private String departamento = "";
        private String codigoPostal = "";
        private String localidad = "";
        private Decimal id;
        private String query;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private SqlParameter parametroOutput;
        private SqlCommand command;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public void SetCalle(String calle)
        {
            this.calle = calle;
        }

        public void SetNumero(String numero)
        {
            this.numero = numero;
        }

        public void SetPiso(String piso)
        {
            this.piso = piso;
        }

        public void SetDepartamento(String departamento)
        {
            this.departamento = departamento;
        }

        public void SetCodigoPostal(String codigoPostal)
        {
            this.codigoPostal = codigoPostal;
        }

        public void SetLocalidad(String localidad)
        {
            this.localidad = localidad;
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

        public Decimal Crear()
        {
            query = "LOS_SUPER_AMIGOS.crear_direccion";
            parametros.Clear();
            parametroOutput = new SqlParameter("@direccion_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@calle", calle));
            parametros.Add(new SqlParameter("@numero", Convert.ToDecimal(numero)));
            parametros.Add(new SqlParameter("@piso", Convert.ToDecimal(piso)));
            parametros.Add(new SqlParameter("@depto", departamento));
            parametros.Add(new SqlParameter("@cod_postal", codigoPostal));
            parametros.Add(new SqlParameter("@localidad", localidad));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            return (Decimal)parametroOutput.Value;
        }

        public Boolean ObtenerDireccion(Decimal idDireccion)
        {
            query = "SELECT calle, numero, piso, depto, cod_postal, localidad FROM LOS_SUPER_AMIGOS.Direccion WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            SqlDataReader readerDireccion = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (readerDireccion.Read())
            {
                this.calle = (String)readerDireccion["calle"];
                this.numero = (String)readerDireccion["numero"];
                this.piso = (String)readerDireccion["piso"];
                this.departamento = (String)readerDireccion["depto"];
                this.codigoPostal = (String)readerDireccion["cod_postal"];
                this.localidad = (String)readerDireccion["localidad"];
                return true;
            }
            return false;
        }
    }
}
