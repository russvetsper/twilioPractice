using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twilioPractice
{
    public class Message
    {

        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }



        static void Main(string[] args)

        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            //1
            var request = new RestRequest("Accounts/ACd6e026fd11ce047ed8523c5390dfac81/Messages.json", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator("ACd6e026fd11ce047ed8523c5390dfac81", "81153f88031ea5c1de2d867fd07a6bdc");
            //2
            var response = new RestResponse();

            //3a-The request is made with an asynchronous method, and Task.Run with Wait() allows us to await asynchronous calls in a "synchronous" way. 
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var messageList = JsonConvert.DeserializeObject<List<Message>>(jsonResponse["messages"].ToString());
            foreach (var message in messageList)
            {
                Console.WriteLine("To: {0}", message.To);
                Console.WriteLine("From: {0}", message.From);
                Console.WriteLine("Body: {0}", message.Body);
                Console.WriteLine("Status: {0}", message.Status);
            }
            Console.ReadLine();
        }


    

        //3b- We set response equal to the response from our request, which we make in the method shown in 3b, and then cast as the type RestResponse.
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        // deserializing- We can actually pull this array out as a JSON object 


    }
}



//Note: replace the placeholders in the code above enclosed within double curly brackets with actual values. For instance, "Accounts/AB1c2d3e45f6gh789i012jk345l678901m/Messages" in place of "Accounts/{{Account SID}}/Messages" and request.AddParameter("To", "+15555555555"); in place of request.AddParameter("To", "{{recipient's phone number}}");.
//The phone numbers need to be in the format "+15555555555" to make the request.
