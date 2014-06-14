using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaCommerce.Objetos
{
    class Usuario : Comunicable
    {
        private Decimal id;
        private String username;
        private String password;

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        public void SetUsername(String username)
        {
            this.username = username;
        }

        public String GetUsername()
        {
            return this.username;
        }

        public void SetPassword(String password)
        {
            this.password = password;
        }

        public String GetPassword()
        {
            return this.password;
        }

        #region Miembros de Comunicable

        string Comunicable.GetQuery()
        {
            return "LOS_SUPER_AMIGOS.crear_usuario";
        }

        IList<System.Data.SqlClient.SqlParameter> Comunicable.GetParametros()
        {
            return new IList<System.Data.SqlClient.SqlParameter>;
        }

        #endregion
    }
}
