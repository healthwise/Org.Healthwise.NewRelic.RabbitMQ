using NewRelic.Platform.Sdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace org.healthwise.newrelic.rabbitmq
{
    class RMQ
    {
        private HttpClient client;

        public RMQ(string protocol, string host, int port, string username, string password)
        {
            // HTTP Client Settings
            var credentials = new NetworkCredential(username, password);
            var handler = new HttpClientHandler { Credentials = credentials };
            var url = string.Format("{0}://{1}:{2}@{3}:{4}", protocol, username, password, host, port);

            // Create HTTP client for future requests to the API            
            this.client = new HttpClient(handler);
            this.client.BaseAddress = new Uri(url);

            // Add an accept header for JSON format.
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T fetchRMQObject<T>(string href)
        {
            HttpResponseMessage response = client.GetAsync(href).Result; // Blocking call
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.  Blocking
                return response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                Console.WriteLine("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
                throw new ApplicationException("Error retrieving data from RabbitMQ");
            }
        }
    }
}
