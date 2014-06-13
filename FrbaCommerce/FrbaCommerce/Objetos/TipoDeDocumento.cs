using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaCommerce.Objetos
{
    class TipoDeDocumento
    {
        private Decimal id;
        private String nombre;

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        public void SetNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public String GetNombre()
        {
            return this.nombre;
        }
    }
}
