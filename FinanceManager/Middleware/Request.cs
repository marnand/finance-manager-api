namespace FinanceManager.Middleware;

/// <summary>
/// Classe de requisições
/// </summary>
/// <param name="next"></param>
public class Request(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    /// <summary>
    /// Inicia a requisição
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;
            httpContext.Response?.WriteAsJsonAsync(new
            {
                Status = 500,
                Message = ex?.Message ?? "Erro no servidor!"
            });
        }        
    }
}
