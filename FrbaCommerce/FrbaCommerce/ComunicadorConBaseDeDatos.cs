using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using FrbaCommerce.Objetos;

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
            query = "SELECT id FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE nombre = @tipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento.GetNombre()));
            return (Decimal) builderDeComandos.Crear(query, parametros).ExecuteScalar();
        }
    }
}
