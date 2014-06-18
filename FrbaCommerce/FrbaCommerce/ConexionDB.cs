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
            this.Conexion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FrbaCommerce.Properties.Settings.GD1C2014ConnectionString"].ConnectionString;
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
