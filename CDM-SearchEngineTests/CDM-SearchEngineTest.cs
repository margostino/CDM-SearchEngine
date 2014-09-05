using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Citrix;
using Nest;

namespace CitrixTest
{
    [TestClass]
    public class SearchEngineTests
    {
        [TestMethod]
        public void testExistIndex()
        {
            
            var myEngine = SearchEngine.getInstance();

            var movie = new Citrix.SearchEngine.Movie
            {
                Id = "2",
                Title = "S",
                Director = "dddd",
                Year = "2013"
            };

            Console.WriteLine(myEngine.getRequest(movie).Created);

            Console.WriteLine("Key...");
            //Console.ReadKey();
            Assert.IsTrue(true);
        }
    }
}
