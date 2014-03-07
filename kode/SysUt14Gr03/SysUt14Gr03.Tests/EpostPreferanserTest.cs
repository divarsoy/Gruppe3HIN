using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class EpostPreferanserTest
    {
        EpostPreferanser pref;

        [TestInitialize()]
        public void Initialize()
        {
            pref = new EpostPreferanser();
        }
        [TestCleanup()]
        public void Cleanup()
        {
            pref = null;
        }

        [TestMethod]
        public void TestGetSetBrukerId()
        {
            string brukernavn = "båb-kåre";
            pref.brukernavn = brukernavn;

            Assert.AreEqual(brukernavn, pref.brukernavn);
        }

        [TestMethod]
        public void TestGetSetSelected()
        {
            bool[] selected = { true, false, true, false, false, true };
            pref.selectedItems = selected;

            Assert.AreEqual(selected, pref.selectedItems);
        }

        [TestMethod]
        public void TestLagring()
        {
            string brukernavn = "båb-kåre";
            bool[] selected = { true, false, true, false, false, true };
            pref.brukernavn = brukernavn;
            pref.selectedItems = selected;

            string expected = "båb-kåre";
            for (int i = 0; i < selected.Length; i++)
                expected += " " + selected[i].ToString();

            Assert.AreEqual(expected, pref.lagrePreferanser(true));
        }
    }
}
