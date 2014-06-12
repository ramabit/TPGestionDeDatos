using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using FrbaCommerce.Objetos;
using FrbaCommerce.Exceptions;
using System.Data;

namespace FrbaCommerce
{
    class ComunicadorConBaseDeDatos
    {
        private String query;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private SqlParameter parametroOutput;
        private SqlCommand command;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public Decimal ObtenerIdDe(TipoDeDocumento tipoDeDocumento)
        {
            if (tipoDeDocumento.GetNombre() == "") 
                throw new FaltanDefinirAtributosException();
            query = "SELECT id FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE nombre = @tipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento.GetNombre()));
            Decimal idTipoDeDocumento = (Decimal)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            tipoDeDocumento.SetId(idTipoDeDocumento);
            return idTipoDeDocumento;
        }

        public String ObtenerNombreDe(TipoDeDocumento tipoDeDocumento)
        {
            if (tipoDeDocumento.GetId() == 0)
                throw new FaltanDefinirAtributosException();
            query = "SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE id = @idTipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idTipoDeDocumento", tipoDeDocumento.GetId()));
            String nombreTipoDeDocumento = (String)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            tipoDeDocumento.SetNombre(nombreTipoDeDocumento);
            return nombreTipoDeDocumento;
        }

        public Decimal CrearDireccion(Direccion direccion)
        {
            query = "LOS_SUPER_AMIGOS.crear_direccion";
            parametros.Clear();
            parametroOutput = new SqlParameter("@direccion_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@calle", direccion.GetCalle()));
            parametros.Add(new SqlParameter("@numero", direccion.GetNumero()));
            parametros.Add(new SqlParameter("@piso", direccion.GetPiso()));
            parametros.Add(new SqlParameter("@depto", direccion.GetDepartamento()));
            parametros.Add(new SqlParameter("@cod_postal", direccion.GetCodigoPostal()));
            parametros.Add(new SqlParameter("@localidad", direccion.GetLocalidad()));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idDireccionNueva = (Decimal)parametroOutput.Value;
            direccion.SetId(idDireccionNueva);
            return idDireccionNueva;
        }

        public Boolean ModificarDireccion(Decimal idDireccion, Direccion direccion)
        {
            query = "UPDATE LOS_SUPER_AMIGOS.Direccion SET calle = @calle, numero = @numero, piso = @piso, depto = @departamento, cod_postal = @codigoPostal, localidad = @localidad WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@calle", direccion.GetCalle()));
            parametros.Add(new SqlParameter("@numero", direccion.GetNumero()));
            parametros.Add(new SqlParameter("@piso", direccion.GetPiso()));
            parametros.Add(new SqlParameter("@departamento", direccion.GetDepartamento()));
            parametros.Add(new SqlParameter("@codigoPostal", direccion.GetCodigoPostal()));
            parametros.Add(new SqlParameter("@localidad", direccion.GetLocalidad()));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) return true;
            return false;
        }
    }
}
