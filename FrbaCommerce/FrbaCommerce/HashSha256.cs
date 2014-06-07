using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FrbaCommerce
{
    class HashSha256
    {
        public static String getHash(String password)
        {
            SHA256Managed encripta = new SHA256Managed();
            string hash = String.Empty;
            byte[] encriptacion = encripta.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in encriptacion)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }
    }

    

}
