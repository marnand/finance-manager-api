using FinanceManager.Model.Control;
using System.Text.RegularExpressions;
using System.Net;

namespace FinanceManager.Model;

public partial class User
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public static Result<User> Create(string name, string email, string pass)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
        {
            return Result<User>.ResultError(
                "O nome deve conter pelo menos 3 caracteres!",
                HttpStatusCode.UnprocessableEntity
            );
        }

        if (!EmailRegex().IsMatch(email))
        {
            return Result<User>.ResultError(
                "Email inválido!",
                HttpStatusCode.UnprocessableEntity
            );
        }

        if (!PasswordRegex().IsMatch(pass))
        {
            return Result<User>.ResultError(
                "A senha deve conter pelo menos 8 caracteres, incluindo números, letras e caracteres especiais!",
                HttpStatusCode.UnprocessableEntity
            );
        }

        return Result<User>.Success(new User()
        {
            Name = name,
            Email = email,
            Password = pass,
            CreatedAt = DateTime.Now
        });
    }

    public void SetPasswordHash(string hash) => Password = hash;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "pt-BR")]
    protected static partial Regex EmailRegex();

    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{8,}$", RegexOptions.Compiled)]
    protected static partial Regex PasswordRegex();
}
