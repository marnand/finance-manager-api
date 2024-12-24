using FinanceManager.Model;
using FinanceManager.Model.Control;
using FinanceManager.Model.DTO;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Interfaces;
using System.Net;

namespace FinanceManager.Service;

internal class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<int>> Create(CreateUserDTO user)
    {
        var result = User.Create(user.Name, user.Email, user.Pass);
        if (!result.IsSuccess)
            return Result<int>.ResultError(result.Error!.Message, result.Error.StatusCode);
        
        var passHash = PasswordHasher.HashPassword(user.Pass);
        result.Data!.SetPasswordHash(passHash);
        return Result<int>.Success(await _userRepository.Create(result.Data));
    }

    public async Task<Result<User>> Login(LoginDTO login)
    {
        var user = await _userRepository.GetByEmail(login.Email); 
        
        if (user is null) return Result<User>.ResultError("Usuário não encontrado!", HttpStatusCode.NotFound);        

        var (isValid, message) = PasswordHasher.VerifyPassword(login.Password, user.PasswordHash);
        if (!isValid) return Result<User>.ResultError(message, HttpStatusCode.Unauthorized);

        return Result<User>.Success(user);
    }

    public async Task<Result<IEnumerable<User>>> Get(int id)
    {
        var users = await _userRepository.Get(id);
        return Result<IEnumerable<User>>.Success(users);
    }
}
