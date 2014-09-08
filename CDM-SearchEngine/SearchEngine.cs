using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Services.Client;
using System.Dynamic;
using System.Linq.Expressions;

using Nest;
using CDM_SearchEngine.Northwind;
using Elasticsearch.Net;
using Newtonsoft.Json;
using LinqKit;

namespace CDM_SearchEngine
{
    public class SearchEngine
    {
        private static SearchEngine instance = null;
	    //private ElasticClient client;	    
        private ElasticsearchClient client;	    

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
        private const String SERVICE_OD_URL_NORTH = "http://services.odata.org/Northwind/Northwind.svc/";
        private const String SERVICE_OD_URL_STORE = "http://services.odata.org/V2/(S(r2ir5rzsz3ygo1dahemljxgj))/OData/OData.svc/";
        private const String INDEX_NORTH = "northwind";
        private const String INDEX_STORE = "store";
	    private const String USED_FORMAT = APPLICATION_JSON;
        public NorthwindEntities context;

        // Define the URI of the public Northwind OData service.
        private Uri northwindUri = new Uri(SERVICE_OD_URL_NORTH, UriKind.Absolute);
        private Uri storeUri = new Uri(SERVICE_OD_URL_STORE, UriKind.Absolute);
        // Define the URI of the public ElasticSearch service.
        private Uri hostEs = new Uri("http://192.168.0.186:9200", UriKind.Absolute);
        
        public SearchEngine() {	      
    		startupES();
            createInstanceOD();		
	    }

        public static SearchEngine getInstance() {
		    if(instance == null) {	
			    instance = new SearchEngine();
    		}
		    return instance;
	    }

        private void startupES(){
    	// on startup    	    	    
            var settings = new ConnectionSettings(hostEs).SetDefaultIndex("peliculas");
            client = new ElasticsearchClient();// ElasticClient(settings);
        }

        public void createInstanceOD()
        {
            // Define the URI of the public Northwind OData service.
            context = new NorthwindEntities(northwindUri);
        }

        public int? getRequestStatus(String index, String type, String id, Object request)
        {
            return client.Get(index, type, id).HttpStatusCode;                
        }

        public int? postRequestStatus(String index, String type, String id, Object request)
        {
            var indexResponse = client.Index(index, type, id, request);
            return indexResponse.HttpStatusCode;
        }

        public bool existDocumentOnES(String index, String type, String id, Object request)
        {                        
            //string sss = getResponse.Response["_source"];            
            return client.Get(index, type, id).Success;           
        }

        public bool existDocumentOnOD(String index, String type, String id, Object request)
        {
            //northwind + customers + alfki (index+type+id)
            
            /*IQueryable<String> query = from o in context.Customers
                                       where o.CustomerID == "ALFKI"
                                       select o;

            var keywords = new List<string>() { "Test1", "Test2" };

            var predicate = PredicateBuilder.False(query);
            
            foreach (var key in keywords)
            {
                predicate = predicate.Or(a => a.Text.Contains(key));
            }

            var query2 = context.Customers.AsQueryable().Where(predicate);*/

            return client.Get(index, type, id).Success;
        }   

    }
}