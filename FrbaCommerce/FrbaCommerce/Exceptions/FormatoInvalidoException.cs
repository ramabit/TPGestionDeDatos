﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaCommerce.Exceptions
{
    class FormatoInvalidoException : Exception
    {
        public FormatoInvalidoException(String mensaje)
            : base(mensaje)
        {

        }
    }
}
