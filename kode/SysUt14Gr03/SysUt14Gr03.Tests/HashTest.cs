using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysUt14Gr03.Classes;
using System.Collections;

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class HashTest
    {
        [TestMethod]
        public void TestGetSaltReturnererStringMedRiktigLengde()
        {
            string salt = Hash.GetSalt();
            Assert.AreEqual(Konstanter.SALTSIZE/2, salt.Length ); //Deles på 2 da utf-16 tar to bytes
        }

        [TestMethod]
        public void TestGetSaltAndHashIkkeReturnerNull()
        {
            string passord = "Test245";
            Hashtable hashTable = Hash.GetHashAndSalt(passord);
            Assert.IsTrue((string)hashTable["hash"] != null);
            Assert.IsTrue((string)hashTable["salt"] != null);
        }

        [TestMethod]
        public void TestGetHash()
        {
            string passord = "Test245";
            string salt = Hash.GetSalt();
            string hash = Hash.GetHash(passord, salt);

            Assert.IsTrue(hash != null);
        }

        [TestMethod]
        public void TestCheckPassordReturnsTrueIfEqualPasswords()
        {
            string passord1 = "Test245";
            string passord2 = "Test245";
            Hashtable hashTable = Hash.GetHashAndSalt(passord1);
            string hash = (string) hashTable["hash"];
            string salt = (string) hashTable["salt"];
            Assert.IsTrue(Hash.CheckPassord(passord2,hash, salt));
        }

        [TestMethod]
        public void TestCheckPassordReturnsFalseIfDifferentPasswords()
        {
            string passord1 = "Test245";
            string passord2 = "NoeHeltAnnet";
            Hashtable hashTable = Hash.GetHashAndSalt(passord1);
            string hash = (string)hashTable["hash"];
            string salt = (string)hashTable["salt"];
            Assert.IsFalse(Hash.CheckPassord(passord2, hash, salt));
        }
    }
}
