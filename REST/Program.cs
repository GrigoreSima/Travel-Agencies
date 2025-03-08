using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace REST
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            string baseUrl = "http://localhost:8080";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                await GetTrips(client);
                await SaveTrip(client);
                await GetTrip(client, 5);
                await UpdateTrip(client);
                await DeleteTrip(client, 5);
            }
        }

        static async Task GetTrips(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync("/trips");
            Console.WriteLine(response.RequestMessage.RequestUri);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("GET Response: " + responseData);
            }
            else
            {
                Console.WriteLine("GET Request failed: " + response.StatusCode);
            }
        }

        static async Task GetTrip(HttpClient client, long id)
        {
            HttpResponseMessage response = await client.GetAsync($"/trips/{id}");
            Console.WriteLine(response.RequestMessage.RequestUri);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("GET Response: " + responseData);
            }
            else
            {
                Console.WriteLine("GET Request failed: " + response.StatusCode);
            }
        }

        static async Task SaveTrip(HttpClient httpClient)
        {
            using StringContent jsonContent = new(
                JsonConvert.SerializeObject(new
                {
                    landmark = "AAA",
                    transportCompany = "BBB",
                    departureTime = "2024-05-21T10:47:14.588658",
                    price = 0.11,
                    slots = 5
                }),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync(
                "/trips",
                jsonContent);

            Console.WriteLine(response.EnsureSuccessStatusCode().RequestMessage);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");
        }
        
        static async Task UpdateTrip(HttpClient httpClient)
        {
            using StringContent jsonContent = new(
                JsonConvert.SerializeObject(new
                {
                    id = 5,
                    landmark = "AAAD",
                    transportCompany = "BBBD",
                    departureTime = "2024-05-21T10:47:14.588658",
                    price = 0.1,
                    slots = 10
                }),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await httpClient.PutAsync(
                "/trips/5",
                jsonContent);

            Console.WriteLine(response.EnsureSuccessStatusCode().RequestMessage);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");
        }
        
        static async Task DeleteTrip(HttpClient client, long id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"/trips/{id}");
            Console.WriteLine(response.RequestMessage.RequestUri);
            Console.WriteLine("DELETE status: " + response.StatusCode);

        }
    }
}