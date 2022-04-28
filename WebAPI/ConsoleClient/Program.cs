using Newtonsoft.Json;
using System.Text.Json;
using WebAPI.Models;

HttpClient client = new HttpClient();

client.BaseAddress = new Uri("https://localhost:7278/");

await ShowItemsAsync<Category>("api/Categories");

await ShowItemsAsync<Product>("api/Products");

Console.ReadLine();

async Task ShowItemsAsync<T>(string uri)
{
    HttpResponseMessage response = await client.GetAsync(uri);
    var responseString = await response.Content.ReadAsStringAsync();

    var categories = JsonConvert.DeserializeObject<List<T>>(responseString);

    Console.WriteLine("Items");
    foreach (var category in categories)
    {
        Console.WriteLine(JsonConvert.SerializeObject(category, Formatting.Indented));
    }
}
