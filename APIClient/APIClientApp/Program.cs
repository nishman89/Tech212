using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace APIClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region single postcode lookup
            // Encapsualtes the info we need to make an API call
            var restClient = new RestClient("https://api.postcodes.io/");
            //set up our request
            var restRequest = new RestRequest();
            //Set the method (by default, our RestRequest object Method property is GET
            restRequest.Method = Method.Get;
            // headers are KVP
            restRequest.AddHeader("Content-Type", "application/json");
            //assign postcode to a variable
            var postcode = "EC2Y 5AS";
            restRequest.Resource = $"postcodes/{postcode}";
            //executre request and store the response
            var singlePostCodeResponse = restClient.Execute(restRequest);
            Console.WriteLine("Response content (json)");
            Console.WriteLine(singlePostCodeResponse.Content);
            Console.WriteLine();
            Console.WriteLine("Response status (enum)");
            Console.WriteLine(singlePostCodeResponse.StatusCode);
            Console.WriteLine("Response status (int)");
            Console.WriteLine((int)singlePostCodeResponse.StatusCode);
            Console.WriteLine();

            var headers = singlePostCodeResponse.Headers;
            foreach (var header in headers)
            {
                Console.WriteLine(header);
            }

            var responseDateHeader = headers.Where(p => p.Name == "Date")
                .Select(h => h.Value.ToString())
                .FirstOrDefault();
            Console.WriteLine(responseDateHeader);
            #endregion

            #region bulk postcode lookup
            var options = new RestClientOptions("https://api.postcodes.io")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/postcodes/", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            var postcodes = new
            {
                Postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP"}
            };
            // add json body serialises the anonymous to JSON
            request.AddJsonBody(postcodes);

            //request.AddStringBody(body, DataFormat.Json);
            RestResponse bulkPostcodeResponse = await client.ExecuteAsync(request);
            Console.WriteLine(bulkPostcodeResponse.Content);
            #endregion

            #region Converting results to JSON
            var singlePostcodeJsonResponse = JObject.Parse(singlePostCodeResponse.Content);
            Console.WriteLine(singlePostcodeJsonResponse);
            Console.WriteLine("Status Json Body");
            Console.WriteLine(singlePostcodeJsonResponse["status"]);
            Console.WriteLine("parliamentary constituency");
            Console.WriteLine(singlePostcodeJsonResponse["result"]["parliamentary_constituency"]);

            var bulkPostcodesJsonResponse = JObject.Parse(bulkPostcodeResponse.Content);
            Console.WriteLine("\nBulk postcode response content as a JObject");
            Console.WriteLine(bulkPostcodesJsonResponse);

            var adminDistrict = bulkPostcodesJsonResponse["result"][1]["result"]["admin_district"];
            Console.WriteLine($"Admin District of 2nd postcode: {adminDistrict}");
            Console.WriteLine($"1st postcode nuts code: {bulkPostcodesJsonResponse["result"][0]["result"]["codes"]["nuts"]}");

            #endregion

            #region Deserialise to POCO
            var singlePostcodesObjectResponse = JsonConvert.DeserializeObject<SinglePostcodeResponse>(singlePostCodeResponse.Content);
            Console.WriteLine("SinglePostcodeResponse");
            Console.WriteLine(singlePostcodesObjectResponse.status);
            Console.WriteLine(singlePostcodesObjectResponse.result.codes.parliamentary_constituency);


            var bulkPostcodesObjectResponse = JsonConvert.DeserializeObject<BulkPostcodeResponse>(bulkPostcodeResponse.Content);
            Console.WriteLine("BulkPostcodeResponse");
            Console.WriteLine(bulkPostcodesObjectResponse.Status);
            foreach (var item in bulkPostcodesObjectResponse.result)
            {
                Console.WriteLine(item.query);
                Console.WriteLine(item.result.admin_district);
            }
            #endregion
        }
    }
}