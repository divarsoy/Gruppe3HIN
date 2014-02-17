using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class prosjektTest
    {
        [TestMethod]
        public void TestSetteNavnPaaProsjektet()
        {
            Prosjekt prosjekt = new Prosjekt();

            prosjekt.Name = "Blaa Himmel";

            string expected = "Blaa Himmel";

            Assert.AreEqual(expected, prosjekt.Name);
        }
    }
}
