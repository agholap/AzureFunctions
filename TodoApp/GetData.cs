using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System;
using Microsoft.ApplicationInsights;

namespace TodoApp
{
    public static class GetData
    {
        public static ITelemetryClientFactory telemetryFactory = new TelemetryClientFactory();
        [FunctionName("GetData")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            string name = ""; List<Contact> contacts = null;
            TelemetryClient telemetryClient = telemetryFactory.GetClient();
            try
            {
                log.Info("C# HTTP trigger function processed a request. - after function updated..");

                // parse query parameter
                name = req.GetQueryNameValuePairs()
                    .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                    .Value;

                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                telemetryClient.TrackEvent("Inside GetData function");
                // Set name to query string or body data
                name = name ?? data?.name;
                contacts = new List<Contact>();

                contacts.Add(new Contact { ContactId = 1, Name = "Aakash" });
            contacts.Add(new Contact { ContactId = 2, Name = "Aashay" });
   

            }
            catch(Exception ex)
            {
                telemetryClient.TrackException(ex);
            }
                return name == null
                    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                    : req.CreateResponse(HttpStatusCode.OK, Newtonsoft.Json.JsonConvert.SerializeObject(contacts));
            }
            
    }

    public class Contact 
    {
        public int ContactId { get; set; }

        public string Name { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
