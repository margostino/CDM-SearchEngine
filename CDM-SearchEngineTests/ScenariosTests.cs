using CDM_SearchEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDM_SearchEngineTests
{
    static class ScenariosTests
    {
        static SearchEngine myEngine;

        public static void setEngine(SearchEngine engine4Tests)
        {
            myEngine = engine4Tests;
        }

        public static String executeQueryPostalCodeOD(IQueryable<String> query)
        {
            return (String)query.First<String>();
        }

        public static IQueryable<String> defineQueryPostalCodeOD()
        {
            // Create a LINQ query to get...            
            IQueryable<String> query = from o in myEngine.context.Customers
                                       where o.CustomerID == "ALFKI"
                                       select o.PostalCode;

            /*IQueryable query2 = from o in myEngine.context.Customers
                                        where o.PostalCode == "12209"
                                       select o;*/

            return query;
        }
    }
}
