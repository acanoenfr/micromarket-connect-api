namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;

internal interface ITestsClient
{
    HttpClient Client { get; }
    Task InitializeStaff();
    Task<HttpResponseMessage> Get(string path, object? parameters = null);
    Task<HttpResponseMessage> Put(string path, object? parameters = null);
    Task<HttpResponseMessage> Post(string path, object? parameters = null);
    Task<HttpResponseMessage> Patch(string path, object? parameters = null);
    Task<HttpResponseMessage> Delete(string path, object? parameters = null);
    Task Initialize();

    void AuthenticateUser(string userId, string[] roles);
}
