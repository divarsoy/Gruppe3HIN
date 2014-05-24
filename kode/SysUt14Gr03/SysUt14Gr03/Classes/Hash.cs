using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

namespace SysUt14Gr03.Classes
{
    /// <summary>
    /// Setter hash og salte passordet her og retunere den saltet/hashet stringen tilbake
    /// </summary>
    static public class Hash
    {
        private static RNGCryptoServiceProvider randomGenerator = new RNGCryptoServiceProvider();

        static public string GetSalt()
        {
            // Create an array of random values.
            UnicodeEncoding utf16 = new UnicodeEncoding();

            byte[] saltValue = new byte[Konstanter.SALTSIZE];
            randomGenerator.GetBytes(saltValue);
            string salt = utf16.GetString(saltValue);
            return salt;
        }

        static public Hashtable GetHashAndSalt(string passord)
        {
            UnicodeEncoding utf16 = new UnicodeEncoding();
            string salt = GetSalt();
            byte[] hashValue;
            byte[] passordByte = utf16.GetBytes(passord+salt);

            SHA512Managed hashString = new SHA512Managed();
            string hash = "";

            hashValue = hashString.ComputeHash(passordByte);
            foreach (byte x in hashValue)
            {
                hash += String.Format("{0:x2}", x);
            }
            Hashtable hashtable = new Hashtable();
            hashtable.Add("hash", hash);
            hashtable.Add("salt", salt);

            return hashtable;
        }

        static public string GetHash(string passord, string salt)
        {
            UnicodeEncoding utf16 = new UnicodeEncoding();
            byte[] hashValue;
            byte[] passordByte = utf16.GetBytes(passord + salt);

            SHA512Managed hashString = new SHA512Managed();
            string hash = "";

            hashValue = hashString.ComputeHash(passordByte);
            foreach (byte x in hashValue)
            {
                hash += String.Format("{0:x2}", x);
            }

            return hash;
        }

        static public bool CheckPassord(string passord, string hash, string salt){
            string hash2 = GetHash(passord, salt);
            return hash.Equals(hash2);
        }
    }
}