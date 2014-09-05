using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Citrix;
using Nest;
using CDM_SearchEngine.Northwind;

namespace CitrixTest
{
    [TestClass]
    public class SearchEngineTests
    {
        [TestMethod]
        public void testRequestIndex()
        {
            
            var myEngine = SearchEngine.getInstance();

            var movie = new Citrix.SearchEngine.Movie
            {
                Id = "2",
                Title = "S",
                Director = "dddd",
                Year = "2013"
            };
            Assert.IsTrue(myEngine.getRequest(movie).Created);
        }

        [TestMethod]
        public void testSearchOData()
        {

            var myEngine = SearchEngine.getInstance();
            myEngine.createInstanceOD();
            
            var query = myEngine.defineQueryExpandOD("Customer");
            
            //myEngine.executeQueryOD(query);

            //Assert.IsTrue(myEngine.getRequest(movie).Created);
        }

    }
}
