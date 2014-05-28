using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FrbaCommerce
{
    class BuilderDeComandos
    {
        private SqlCommand command { get; set; }
        private ConexionDB conexion = new ConexionDB();

        public SqlCommand Crear(string sqlTexto, IList<SqlParameter> parametros)
        {
            this.command = new SqlCommand();
            this.command.CommandText = sqlTexto;
            if (parametros != null)
            {
                foreach (SqlParameter parametro in parametros)
                {
                    this.command.Parameters.Add(parametro);
                }
            }
            if (this.command.Connection == null) this.command.Connection = conexion.AbrirConexion();

            return this.command;
        }
    }
}