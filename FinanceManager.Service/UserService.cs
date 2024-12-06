using FinanceManager.Model;
using FinanceManager.Model.DTO;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Extensions;
using FinanceManager.Service.Interfaces;
using System.Net;

namespace FinanceManager.Service;

internal class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<int>> Create(CreateUserDTO user)
    {
        var passHash = new PasswordHasher().HashPassword(user.Pass);
        var userCreated = User.Factories.Create(user.Name, user.Email, passHash);
        
        return Result<int>.Success(await _userRepository.Create(userCreated));
    }

    public async Task<Result<User>> Login(LoginDTO login)
    {
        var user = await _userRepository.GetByEmail(login.Email); 
        
        if (user is null) return Result<User>.ResultError("Usuário não encontrado!", HttpStatusCode.NotFound);        

        var (isValid, message) = new PasswordHasher().VerifyPassword(login.Password, user.PasswordHash);
        if (!isValid) return Result<User>.ResultError(message, HttpStatusCode.Unauthorized);

        return Result<User>.Success(user);
    }

    public async Task<User> Edit()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<User>>> Get(int id)
    {
        var users = await _userRepository.Get(id);
        return Result<IEnumerable<User>>.Success(users);
    }

    public async Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}
