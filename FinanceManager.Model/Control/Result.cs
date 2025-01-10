using System.Net;

namespace FinanceManager.Model.Control;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public ErrorDetails? Error { get; private set; }

    private Result(T data)
    {
        IsSuccess = true;
        Data = data;
        Error = null;
    }

    private Result(ErrorDetails error)
    {
        IsSuccess = false;
        Data = default;
        Error = error;
    }

    public static Result<T> Success(T data) => new(data);

    public static Result<T> ResultError(string message, HttpStatusCode statusCode) =>
        new(new ErrorDetails { Message = message, StatusCode = statusCode });

    public class ErrorDetails
    {
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
    }

    public TResult Match<TResult>(Func<Result<T>, TResult> onSuccess, Func<Result<T>, TResult> onError)
        => IsSuccess ? onSuccess(new Result<T>(Data!)) : onError(new Result<T>(Error!));
}
