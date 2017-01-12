using System;
using System.Configuration;
using System.Net.NetworkInformation;
using SOAPQueryTest.GenevaSoap;


namespace SOAPQueryTest
{



    class Program {



        static void Main(string[] args) {
            GenevaSoapServiceClient     client = null;
            GenevaConnectionParameters  connectionParams = null;
            string sessionId = null,
                queryText = null;

            try {
                client = new GenevaSoapServiceClient("GenevaBasicHttpEndPoint");
                Console.WriteLine("\r\n\r\nCreated GenevaSoapServiceClient");

                connectionParams = new GenevaConnectionParameters() {
                    GenevaHost = ConfigurationManager.AppSettings["host"],
                    GenevaPort = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
                    Username = ConfigurationManager.AppSettings["username"],
                    Password = ConfigurationManager.AppSettings["password"]
                };

                sessionId = client.GenevaConnection(connectionParams);
                Console.WriteLine("\r\n\r\nConnected with SessionID: {0}", sessionId);

                Console.Write("Enter query text: ");
                queryText = Console.ReadLine();

                Console.WriteLine("Executing GSQL query: {0}", queryText);
                var resultGSQL = client.ExecuteGSQL(sessionId, queryText);
                Console.WriteLine("\r\n\r\n");
                Console.WriteLine(resultGSQL.Result);

            } catch(Exception e) {
                Console.WriteLine(e);

            } finally {
                if(client != null && !string.IsNullOrEmpty(sessionId)) {
                    client.Close(sessionId);
                }

            }

            Console.WriteLine("\r\n\r\nDone");
            Console.ReadLine();

        }

    }

}