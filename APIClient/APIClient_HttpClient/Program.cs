﻿using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace APIClient_HttpClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                // Set up the request
                var singlePostcodeRequest = new HttpRequestMessage
                {
                    // Set request method to Get
                    Method = HttpMethod.Get
                };
                // Add accept header to HTTP request, so that the api knows we are expecting a JSON.
                singlePostcodeRequest.Headers.Add("Accept", "application/json");
                var postcode = "EC2Y 5AS";
                // Setting up the request URI
                singlePostcodeRequest.RequestUri = new Uri($"https://api.postcodes.io/postcodes/{postcode}");

                try
                {
                    HttpResponseMessage singlePostcodeResponse = await client.SendAsync(singlePostcodeRequest);
                    Console.WriteLine("Status codes");
                    Console.WriteLine(singlePostcodeResponse.StatusCode);
                    Console.WriteLine((int)singlePostcodeResponse.StatusCode);

                    if (singlePostcodeResponse.IsSuccessStatusCode)
                    {
                        // Read response content as a string and assign to variable
                        string singlePostcodeContent = await singlePostcodeResponse.Content.ReadAsStringAsync();
                        // Print out string body
                        Console.WriteLine("\nSingle postcode response body content");
                        Console.WriteLine(singlePostcodeContent);
                        // Assign headers
                        HttpHeaders singlePostcodeHeaders = singlePostcodeResponse.Headers;
                        Console.WriteLine("\nSingle postcode response headers");
                        Console.WriteLine(singlePostcodeHeaders);
                        // Check date header
                        string dateHeader = singlePostcodeHeaders.GetValues("Date").FirstOrDefault();
                        Console.WriteLine(dateHeader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}