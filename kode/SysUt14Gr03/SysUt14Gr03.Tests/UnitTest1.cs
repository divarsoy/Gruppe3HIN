using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysUt14Gr03;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq; 

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void TestSjekkRettighet(Bruker bruker, Konstanter.rettighet rettighet)
        {
            var rettigheter = new List <Rettighet>
            { 
                new Rettighet{ RettighetNavn = "Brukeradmin"},
                new Rettighet{ RettighetNavn = "Prosjektleder"},
                new Rettighet {RettighetNavn = "Utvikler"}
            }.AsQueryable();

            var mockBruker = new Mock<DbSet<Bruker>>();
            var mockRettighet = new Mock<DbSet<Rettighet>>();
            mockRettighet.As<IQueryable<Rettighet>>().Setup(m => m.Provider).Returns(rettigheter.Provider);
            mockRettighet.As<IQueryable<Rettighet>>().Setup(m => m.Expression).Returns(rettigheter.Expression);
            mockRettighet.As<IQueryable<Rettighet>>().Setup(m => m.ElementType).Returns(rettigheter.ElementType);
            mockRettighet.As<IQueryable<Rettighet>>().Setup(m => m.GetEnumerator()).Returns(rettigheter.GetEnumerator());

            var mockContext = new Mock<Context>();
            mockContext.Setup(c => c.Rettigheter).Returns(mockRettighet.Object);

            /*
             * Work in progress: Se http://msdn.microsoft.com/en-us/data/dn314429.aspx for mer info for å implementere et mock objekt.
            var service = new BlogService(mockContext.Object);
            var blogs = service.GetAllBlogs();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].Name);
            Assert.AreEqual("BBB", blogs[1].Name);
            Assert.AreEqual("ZZZ", blogs[2].Name); 
             * */

        }
    }
}
