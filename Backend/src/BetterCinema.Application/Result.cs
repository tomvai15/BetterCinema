namespace BetterCinema.Application;

public class Result<T> : Result where T : class
{
    public T? Data { get; set; }

    public static Result<T> OkResult(T data)
    {
        return new Result<T> { Data = data, StatusCode = StatusCode.Ok };
    }

    public static Result<T> BadResult(StatusCode statusCode, string message)
    {
        return new Result<T> { StatusCode = statusCode, Message = message };
    }
}

public class Result
{
    public StatusCode StatusCode { get; set; }
    public string? Message { get; set; }

    public bool IsSuccessful()
    {
        var statusCodeInt = (int)StatusCode;
        return statusCodeInt is >= 200 and <= 299;
    }
}

public enum StatusCode
{
    Ok = 200,
    NotFound = 404,
    BadRequest = 400,
    InternalServerError = 500,
}