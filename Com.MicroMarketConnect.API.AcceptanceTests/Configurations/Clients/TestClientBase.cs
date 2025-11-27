using Flurl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reqnroll;
using System.Text;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;

internal class TestClientBase : IDisposable
{
    public HttpClient Client { get; }

    private readonly ScenarioContext _context;

    protected TestClientBase(ScenarioContext context, HttpClient client)
    {
        _context = context;
        Client = client;
    }

    public async Task<HttpResponseMessage> Post(string path, object? parameters = null) =>
        await ExecuteRequest(HttpMethod.Post, path, parameters);

    public async Task<HttpResponseMessage> Patch(string path, object? parameters = null) =>
        await ExecuteRequest(HttpMethod.Patch, path, parameters);

    public async Task<HttpResponseMessage> Put(string path, object? parameters = null) =>
        await ExecuteRequest(HttpMethod.Put, path, parameters);

    public async Task<HttpResponseMessage> Delete(string path, object? parameters = null) =>
        await ExecuteRequest(HttpMethod.Delete, path, parameters);

    public async Task<HttpResponseMessage> Get(string path, object? parameters = null) =>
        await ExecuteRequest(HttpMethod.Get, path, parameters);

    private async Task<HttpResponseMessage> ExecuteRequest(HttpMethod method, string path, object? parameters = null)
    {
        try
        {
            using HttpRequestMessage request = BuildMessage(method, path, parameters);
            HttpResponseMessage response = await Client.SendAsync(request);
            await RecordErrorIfAny(response);
            return response;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
        }

        return new HttpResponseMessage();
    }

    private static HttpRequestMessage BuildMessage(HttpMethod method, string path, object? parameters = null)
    {
        return BuildJsonMessage(method, path, parameters);
    }

    private static HttpRequestMessage BuildJsonMessage(HttpMethod method, string path, object? parameters = null)
    {
        if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch || method == HttpMethod.Delete)
        {
            HttpRequestMessage request = new(method, new Uri(path, UriKind.RelativeOrAbsolute));
            if (parameters != null)
            {
                request.Content = parameters is Stream stream
                    ? new StreamContent(stream)
                    : new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
            }

            return request;
        }

        if (parameters != null)
        {
            path = path.SetQueryParams(parameters);
        }

        return new HttpRequestMessage(method, new Uri(path, UriKind.RelativeOrAbsolute));
    }

    private async Task RecordErrorIfAny(HttpResponseMessage response)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            await HandleDefaultHttpException();
        }

        async Task HandleDefaultHttpException()
        {
            string content = await response.Content.ReadAsStringAsync();
            ProblemDetails details = JsonConvert.DeserializeObject<ProblemDetails>(content)!;
            _context.SetError(new HttpTestServerException(response.StatusCode, details));
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Client.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
