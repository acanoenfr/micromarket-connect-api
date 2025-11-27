using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

internal class HttpTestServerException(
    HttpStatusCode statusCode,
    ProblemDetails details) : Exception($"Error {(int)statusCode}: {details}")
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public ProblemDetails Details { get; } = details;
}
