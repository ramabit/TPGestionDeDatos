using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FrbaCommerce
{
    class ConexionDB
    {
        private SqlConnection Conexion { get; set; }

        public SqlConnection AbrirConexion()
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = @"Server=localhost\SQLSERVER2008;Database=GD1C2014;User Id=gd; Password=gd2014";
            this.Conexion.Open();

            return this.Conexion;
        }

        public void CerrarConexion()
        {
            if (this.Conexion != null) { 
                this.Conexion.Close(); 
            }
        }
    }
}
