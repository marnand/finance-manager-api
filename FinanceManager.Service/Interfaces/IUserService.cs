using FinanceManager.Model;
using FinanceManager.Model.DTO;
using FinanceManager.Service.Extensions;

namespace FinanceManager.Service.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<User>>> Get(int id = 0);
    Task<Result<int>> Create(CreateUserDTO user);
    Task<Result<User>> Login(LoginDTO login);
    Task<User> Edit();
    Task Remove(int id);
}
