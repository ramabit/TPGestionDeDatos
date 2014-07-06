using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaCommerce.Objetos
{
    class Objeto
    {
        public Boolean esNumero(String numString)
        {
            long number1 = 0;
            return long.TryParse(numString, out number1); // devuelve true si pudo convertirlo, es decir, es numero
        }

        public Boolean esDouble(String numString)
        {
            Double number1 = 0;
            return Double.TryParse(numString, out number1); // devuelve true si pudo convertirlo, es decir, es numero
        }

        public Boolean esFechaPasada(DateTime dateTime)
        {
            DateTime dateNow = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DateKey"]); ;
            int comparacion = dateTime.CompareTo(dateNow);
            if (comparacion >= 0)
                return false;
            else
                return true;
        }
        public Object siEsNuloDevolverDBNull(String campo)
        {
            if (campo == "")
            {
                return DBNull.Value;
            }
            else
            {
                return campo;
            }
        }

        public Boolean esCuit(String cuit)
        {
            if (cuit.Length < 14) return false;
            String primerosDosNumeros = cuit.Substring(0, 2);
            String primerGuion = cuit.Substring(2, 1);
            String ochoNumeros = cuit.Substring(3, 8);
            String segundoGuion = cuit.Substring(11, 1);
            String segundosDosNumeros = cuit.Substring(12, 2);

            return this.esNumero(primerosDosNumeros) && primerGuion == "-" && this.esNumero(ochoNumeros) && segundoGuion == "-" && this.esNumero(segundosDosNumeros);
        }
    }
}
