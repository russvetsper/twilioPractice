using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twilioPractice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1 Make a connection with the server where the API is located.
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            //2 Make a connection with the server where the API is located.
            var request = new RestRequest("Accounts/PNfabfe8b6a7d307d675a3f3f48fea8390/Messages", Method.POST);
            //3 Add parameters to our request.Here we've set the text message's sender, recipient, and actual message.
            request.AddParameter("To", "+3473022537");
            request.AddParameter("From", "+3476798347");
            request.AddParameter("Body", "Hello world!");
            //4 Give the client the appropriate credentials.
            client.Authenticator = new HttpBasicAuthenticator("PNfabfe8b6a7d307d675a3f3f48fea8390", "81153f88031ea5c1de2d867fd07a6bdc");
            //5 Give the client the appropriate credentials.
            client.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response);
            });
            Console.ReadLine();
        }
    }
}



//Note: replace the placeholders in the code above enclosed within double curly brackets with actual values. For instance, "Accounts/AB1c2d3e45f6gh789i012jk345l678901m/Messages" in place of "Accounts/{{Account SID}}/Messages" and request.AddParameter("To", "+15555555555"); in place of request.AddParameter("To", "{{recipient's phone number}}");.
//The phone numbers need to be in the format "+15555555555" to make the request.
