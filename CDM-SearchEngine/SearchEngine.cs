using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nest;
using CDM_SearchEngine.Northwind;
using System.Data.Services.Client;

namespace Citrix
{
    public class SearchEngine
    {
        private static SearchEngine instance = null;
	    private ElasticClient client;	    
	    
        private const String ID_NOT_FOUND = "<ID NOT FOUND>";
	    private const String HTTP_METHOD_PUT = "PUT";
	    private const String HTTP_METHOD_POST = "POST";
	    private const String HTTP_METHOD_GET = "GET";
	    private const String HTTP_METHOD_DELETE = "DELETE";
	    private const String HTTP_HEADER_CONTENT_TYPE = "Content-Type";
	    private const String HTTP_HEADER_ACCEPT = "Accept";

	    private const String APPLICATION_JSON = "application/json";
	    private const String APPLICATION_XML = "application/xml";
	    private const String APPLICATION_ATOM_XML = "application/atom+xml";
	    private const String APPLICATION_FORM = "application/x-www-form-urlencoded";
	    private const String METADATA = "$metadata";
	    private const String INDEX = "/index.jsp";
	    private const String SEPARATOR = "/";    
	
	    private const bool PRINT_RAW_CONTENT = true;
	
	    /*private const String SERVICE_OD_URL = "http://localhost:8080/cars-annotations-sample/MyFormula.svc";*/
        private const String SERVICE_OD_URL = "http://services.odata.org/Northwind/Northwind.svc/";
	    private const String USED_FORMAT = APPLICATION_JSON;
        private NorthwindEntities context;

        // Define the URI of the public Northwind OData service.
        private Uri northwindUri = new Uri(SERVICE_OD_URL, UriKind.Absolute);
        // Define the URI of the public ElasticSearch service.
        private Uri hostEs = new Uri("http://192.168.0.186:9200", UriKind.Absolute);
        
        public SearchEngine() {	      
    		startupES();		    		
	    }

        public static SearchEngine getInstance() {
		    if(instance == null) {	
			    instance = new SearchEngine();
    		}
		    return instance;
	    }

        private void startupES(){
    	// on startup    	    	    
            var settings = new ConnectionSettings(hostEs).SetDefaultIndex("amovie");
            client = new ElasticClient(settings);
        }

        public void createInstanceOD()
        {
            // Define the URI of the public Northwind OData service.
            context = new NorthwindEntities(northwindUri);
        }

        public Object defineQueryExpandOD(String expand)
        {
            // Create a LINQ query to get the orders, including line items,
            //expand="Orders_details" 
            var query = from order in context.Orders.Expand(expand)            
                        select order;

            return query;
        }

        public bool executeQueryOD(Customer query)
        {
            var result = false;
            try
            {                
                // Enumerating returned orders sends the query request to the service.
                /*foreach (Customer c in query)
                {
                    Console.WriteLine(c.ContactName);
                }*/
            }
            catch (DataServiceQueryException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public IIndexResponse getRequest(Object inx)
        {
            return client.Index(inx);
        }
        
        public class Movie
        {
            public Movie()
            {

            }
            public string Id { get; set; }
            public string Title { get; set; }
            public string Director { get; set; }
            public string Year { get; set; }
        }

        public class Article
        {
            public string title { get; set; }
            public string artist { get; set; }
            public Article(string Title, string Artist)
            {
                title = Title; artist = Artist;
            }
        }

        public class Contacts
        {
            public string name { get; set; }
            public string country { get; set; }
            public Contacts(string Name, string Country)
            {
                name = Name; country = Country;
            }
        }

        public static void UpsertArticle(ElasticClient client, Article article, string index, string type, int id)
        {
            /*var RecordInserted = client.Index(article, index, type, id).Id;

            if (RecordInserted.ToString() != "")
            {
                Console.WriteLine("Transaction Successful !");
            }
            else
            {
                Console.WriteLine("Transaction Failed");
            }*/
        }

        public static void UpsertContact(ElasticClient client, Contacts contact, string index, string type, int id)
        {
            /*var RecordInserted = client.Index(contact, index, type, id).Id;

            if (RecordInserted.ToString() != "")
            {
                Console.WriteLine("Transaction Successful !");
            }
            else
            {
                Console.WriteLine("Transaction Failed");
            }*/
        }
    }
}