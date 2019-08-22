using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WildlifePark_MVC.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }


        public static List<Animal> GetAnimals()
        {
            var client = new RestClient("http://localhost:5000/api/animals");
            var request = new RestRequest(Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
            var animalList = JsonConvert.DeserializeObject<List<Animal>>(jsonResponse.ToString());
            return animalList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}