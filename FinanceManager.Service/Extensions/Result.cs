using System.Net;

namespace FinanceManager.Service.Extensions;

public class Result<T>
{
    // Propriedades principais
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public ErrorDetails? Error { get; private set; }

    // Construtores privados para controle interno
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

    // Métodos de fábrica para criar sucesso ou erro
    public static Result<T> Success(T data) => new(data);

    public static Result<T> ResultError(string message, HttpStatusCode statusCode) =>
        new(new ErrorDetails { Message = message, StatusCode = statusCode });

    // Classe de detalhes do erro
    public class ErrorDetails
    {
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
    }

    // Método para aplicar pattern matching
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ErrorDetails, TResult> onError)
        => IsSuccess ? onSuccess(Data) : onError(Error);
}
