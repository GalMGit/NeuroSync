namespace AggregateService.API.DTOs.Errors;

public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }
    public int StatusCode { get; set; }

    public static ServiceResponse<T> Ok(T data)
    {
        return new ServiceResponse<T>
        {
            Success = true,
            Data = data,
            StatusCode = 200
        };
    }

    public static ServiceResponse<T> Fail(string message, string errorCode, int statusCode = 500)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorMessage = message,
            ErrorCode = errorCode,
            StatusCode = statusCode
        };
    }

    public static ServiceResponse<T> NotFound(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorMessage = message,
            ErrorCode = "NOT_FOUND",
            StatusCode = 404
        };
    }

    public static ServiceResponse<T> UserDeleted(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorMessage = message,
            ErrorCode = "USER_DELETED",
            StatusCode = 404
        };
    }

    public static ServiceResponse<T> Unauthorized()
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorMessage = "Необходима авторизация",
            ErrorCode = "UNAUTHORIZED",
            StatusCode = 401
        };
    }

    public static ServiceResponse<T> ServiceUnavailable(string serviceName)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorMessage = $"Сервис {serviceName} временно недоступен",
            ErrorCode = "SERVICE_UNAVAILABLE",
            StatusCode = 503
        };
    }

    public bool IsNotFound => StatusCode == 404 && ErrorCode == "NOT_FOUND";
    public bool IsUserDeleted => StatusCode == 404 && ErrorCode == "USER_DELETED";
    public bool IsUnauthorized => StatusCode == 401;
    public bool IsServiceUnavailable => StatusCode == 503;
}