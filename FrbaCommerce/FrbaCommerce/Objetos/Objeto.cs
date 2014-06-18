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
    }
}
