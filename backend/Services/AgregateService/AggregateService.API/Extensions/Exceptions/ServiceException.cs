using System;
using System.Net;

namespace AggregateService.API.Extensions.Exceptions;

public class ServiceException : Exception
{
    public string Title { get; set; }
    public HttpStatusCode StatusCode { get; }

    public ServiceException(
        string message,
        string title,
        HttpStatusCode statusCode
    ) : base(message)
    {
        Title = title;
        StatusCode = statusCode;
    }
}
