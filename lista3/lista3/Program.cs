using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // URL do trzech różnych API
        string apiUrl1 = "https://jsonplaceholder.typicode.com/todos/1";
        string apiUrl2 = "http://api.weatherapi.com/v1/current.json";
        string apiUrl3 = "https://api.restful-api.dev/objects?id=3&id=5&id=10";

        // Wywołanie metody do pobrania i przetworzenia danych z trzech API
        await FetchAndParseApiData(apiUrl1);
        await FetchAndParseApiData(apiUrl2);
        await FetchAndParseApiData(apiUrl3);
    }

    static async Task FetchAndParseApiData(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
 
                JObject jsonResponse = JObject.Parse(responseBody);

                Console.WriteLine("Dane z API: " + jsonResponse.ToString());

                if (response.IsSuccessStatusCode)
                {
                    ValidateJsonResponse("userId", jsonResponse.ToString());
                    ValidateJsonResponse("name", jsonResponse.ToString());
                    ValidateJsonResponse("id", jsonResponse.ToString());

                }

                Console.ReadKey();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Wystąpił błąd podczas pobierania danych z API: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
        }
    }
    private static void ValidateJsonResponse(string endpointName, string jsonResponse)
    {
        try
        {
            JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse);

           
            switch (endpointName)
            {
                case "userId":
                    foreach (var element in jsonDocument.RootElement.EnumerateArray())
                    {
                        if (!element.TryGetProperty("userId", out JsonElement userId))
                        {
                            Console.WriteLine($"Nie zawiera: {endpointName}");
                        }
                    }
                    break;

                case "name":
                    foreach (var element in jsonDocument.RootElement.EnumerateArray())
                    {
                        if (!element.TryGetProperty("name", out JsonElement postId))
                        {
                            Console.WriteLine($"Nie zawiera: {endpointName}");
                        }
                    }
                    break;

                case "id":
                    foreach (var element in jsonDocument.RootElement.EnumerateArray())
                    {
                        if (!element.TryGetProperty("id", out JsonElement id))
                        {
                            Console.WriteLine($"Nie zawiera: {endpointName}");
                        }
                    }
                    break;

                default:
                    Console.WriteLine($"Nieznany parametr {endpointName}");
                    break;
            }

        }
        catch (System.Text.Json.JsonException e)
        {
            Console.WriteLine($"Błąd w zwrotce z API {endpointName}");
        }
    }
}