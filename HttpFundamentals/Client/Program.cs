using System.Net;

CookieContainer cookies = new CookieContainer();
HttpClientHandler handler = new HttpClientHandler();
handler.CookieContainer = cookies;
var httpClient = new HttpClient(handler);

Console.WriteLine("Task 1:");
await ExecuteTask1();

Console.WriteLine("Task 2:");
await ExecuteTask2();

Console.WriteLine("Task 3:");
await ExecuteTask3();

Console.WriteLine("Task 4:");
await ExecuteTask4();

Console.ReadLine();

async Task ExecuteTask1()
{
    Console.WriteLine("Request URL: http://localhost:8888/MyName");
    var response = await httpClient.GetAsync("http://localhost:8888/MyName");

    Console.WriteLine("Response:");
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

async Task ExecuteTask2()
{
    //await DoRequest("http://localhost:8888/Information");
    await DoRequest("http://localhost:8888/Success");
    await DoRequest("http://localhost:8888/Redirection");
    await DoRequest("http://localhost:8888/ClientError");
    await DoRequest("http://localhost:8888/ServerError");
}

async Task ExecuteTask3()
{
    var response = await httpClient.GetAsync("http://localhost:8888/MyNameByHeader/");

    Console.WriteLine("Name:");
    Console.WriteLine(response.Headers.First(x => x.Key == "X-MyName").Value.First());
}

async Task ExecuteTask4()
{
    var response = await httpClient.GetAsync("http://localhost:8888/MyNameByCookies");

    Console.WriteLine("Name:");
    IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri("http://localhost:8888/MyNameByCookies")).Cast<Cookie>();
    Console.WriteLine(responseCookies.First(x => x.Name == "MyName").Value);
}

async Task DoRequest(string url)
{
    Console.WriteLine($"Request URL: {url}");
    var response = await httpClient.GetAsync(url);
    Console.WriteLine("Response:");
    Console.WriteLine($"Message {await response.Content.ReadAsStringAsync()} with code - {response.StatusCode}");
}
