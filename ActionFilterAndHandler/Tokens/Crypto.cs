using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ActionFilterAndHandler.Tokens
{
    public class Crypto
    {
        public static string Rfc2898(string input)
        {
            byte[] salt;
            byte[] bytes;
            if (input == null)
            {
                throw new ArgumentNullException("Input");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(input, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }
    }
}