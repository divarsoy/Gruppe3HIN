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
        //http://devproconnections.com/aspnet/aspnet-web-security-protect-user-passwords-hashing-and-salt
       
        public static String HashPassord(string passord) 
        {
            var combinedPassword = String.Concat(passord, GetRandomSalt());
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static String GetRandomSalt()
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[12];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        } 

     /*   public static Boolean HashPassordSjekk(string salt)
        {
            //ikke ferdig? what to do????
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(salt);
            var hash = sha256.ComputeHash(bytes);
            return String.Equals(hash, salt);

        } */
        public static String HashPassordSjekk(string salt, string passord)
        {
            var combinesCheckPassword = String.Concat(passord, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinesCheckPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}