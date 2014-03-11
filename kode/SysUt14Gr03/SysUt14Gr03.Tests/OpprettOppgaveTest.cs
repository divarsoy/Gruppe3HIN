using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class OpprettOppgaveTest
    {
        OpprettOppgave oppg;
        [TestMethod]
        public void TestMethod1()
        {
            oppg = new OpprettOppgave();
        }
        [TestCleanup()]
        public void Cleanup()
        {
            oppg = null;
        }
        [TestMethod]
        public void TestGetSetBrukerId()
        {
          //noe mer senere
        }
    }
}
