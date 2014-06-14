using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FrbaCommerce.Objetos
{
    interface Comunicable
    {
        String GetQuery();
        IList<SqlParameter> GetParametros();
    }
}
