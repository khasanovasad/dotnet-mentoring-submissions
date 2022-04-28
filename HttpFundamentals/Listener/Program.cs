using System.Net;
using System.Text;

var listener = new HttpListener();

listener.Prefixes.Add("http://localhost:8888/");
listener.Start();

while (true)
{
    var context = await listener.GetContextAsync();
    HttpListenerResponse response = context.Response;

    switch (context.Request.RawUrl)
    {
        case "/MyName":
            await ReturnMyName(response);
            break;
        case "/Information":
            await ResponseWithInfoStatus(response);
            break;
        case "/Success":
            await ResponseWithSuccessStatus(response);
            break;
        case "/Redirection":
            await ResponseWithRedirectionStatus(response);
            break;
        case "/ClientError":
            await ResponseWithClientErrorStatus(response);
            break;
        case "/ServerError":
            await ResponseWithServerErrorStatus(response);
            break;
        case "/MyNameByHeader/":
            await ResponseWithHeader(response);
            break;
        case "/MyNameByCookies":
            await ResponseWithCookies(response);
            break;
    }
}

async Task ResponseWithCookies(HttpListenerResponse response)
{
    var name = GetMyName();
    response.AppendCookie(new("MyName", name));
    await WriteResponse(response, string.Empty);
}

async Task ResponseWithHeader(HttpListenerResponse response)
{
    var name = GetMyName();
    response.AddHeader("X-MyName", name);
    response.StatusCode = (int)HttpStatusCode.OK;
    await WriteResponse(response, string.Empty); 
}

async Task ResponseWithServerErrorStatus(HttpListenerResponse response)
{
    response.StatusCode = (int)HttpStatusCode.InternalServerError;
    var responseStr = "Internal server error status";

    await WriteResponse(response, responseStr);
}

async Task ResponseWithClientErrorStatus(HttpListenerResponse response)
{
    response.StatusCode=(int)HttpStatusCode.BadRequest;
    var responseStr = "Client error status";

    await WriteResponse(response, responseStr);
}

async Task ResponseWithRedirectionStatus(HttpListenerResponse response)
{
    response.StatusCode = (int)HttpStatusCode.Redirect;
    var responseStr = "Redirection response";

    await WriteResponse(response, responseStr); 
}

async Task ResponseWithSuccessStatus(HttpListenerResponse response)
{
    response.StatusCode = (int)HttpStatusCode.OK;
    var responseStr = "Success response";

    await WriteResponse(response, responseStr);
}

async Task ResponseWithInfoStatus(HttpListenerResponse response)
{
    response.StatusCode = (int)HttpStatusCode.Processing;
    var responseStr = "Information response";

    await WriteResponse(response, responseStr);
}

async Task ReturnMyName(HttpListenerResponse response)
{
    var responseStr = GetMyName();

    await WriteResponse(response, responseStr);
}

async Task WriteResponse(HttpListenerResponse response, string responseStr)
{
    byte[] buffer = Encoding.UTF8.GetBytes(responseStr);
    response.ContentLength64 = buffer.Length;
    var output = response.OutputStream;
    await output.WriteAsync(buffer, 0, buffer.Length);
    output.Close();
}

string GetMyName()
{
    Console.WriteLine("Enter your name");
    return Console.ReadLine();
}