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
    }
}
