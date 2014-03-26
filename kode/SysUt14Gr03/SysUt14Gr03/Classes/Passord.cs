using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SysUt14Gr03.Classes
{
    public class Passord
    {
       
        public static String HashPassord(string passord) {
            
            var combinedPassword = String.Concat(passord, GetRandomSalt());
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);

        }
        private static String GetRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        public static Boolean HashPassordSjekk(string salt)
        {
            //ikke ferdig
            var hash = HashPassord(salt);
            return String.Equals(hash, salt);
        }
    }
}