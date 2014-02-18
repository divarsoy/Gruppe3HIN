using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysUt14Gr03;

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class brukerTest
    {

        BrukerEksempel bruker;

        [TestInitialize()]
        public void Initialize()
        {
            bruker = new BrukerEksempel();
        }


        [TestCleanup()]
        public void Cleanup()
        {
            bruker = null;
        }

        [TestMethod]
        public void TestGetSetEtternavn()
        {
            string etternavn = "Nilsen";
            bruker.Etternavn = etternavn;

            Assert.AreEqual(etternavn, bruker.Etternavn);
        }

        [TestMethod]
        public void TestGetSetFornavn()
        {
            string fornavn = "Martin";
            bruker.Fornavn = fornavn;

            Assert.AreEqual(fornavn, bruker.Fornavn);
        }

        [TestMethod]
        public void TestGetNavn()
        {
            string fornavn = "Martin";
            string etternavn = "Nilsen";
            bruker.Fornavn = fornavn;
            bruker.Etternavn = etternavn;

            string expected = "Martin Nilsen";

            Assert.AreEqual(expected, bruker.Navn());
        }
    }
}
