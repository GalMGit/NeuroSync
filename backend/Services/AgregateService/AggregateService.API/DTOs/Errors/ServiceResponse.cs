namespace AggregateService.API.DTOs.Errors;


public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ErrorCode { get; set; }
    public T? Data { get; set; }
    
    public static ServiceResponse<T> Ok(T data, string message = "Success")
    {
        return new ServiceResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
    
    public static ServiceResponse<T> Fail(string message, string errorCode = "ERROR")
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Data = default
        };
    }
    
    public static ServiceResponse<T> ServiceUnavailable(string serviceName)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = $"Сервис {serviceName} временно недоступен",
            ErrorCode = "SERVICE_UNAVAILABLE",
            Data = default
        };
    }
    
    public static ServiceResponse<T> Timeout(string serviceName)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = $"Сервис {serviceName} не отвечает",
            ErrorCode = "TIMEOUT",
            Data = default
        };
    }
}