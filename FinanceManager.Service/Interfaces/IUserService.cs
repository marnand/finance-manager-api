using FinanceManager.Model;
using FinanceManager.Model.Control;
using FinanceManager.Model.DTO;

namespace FinanceManager.Service.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<User>>> Get(int id = 0);
    Task<Result<int>> Create(CreateUserDTO user);
    Task<Result<User>> Login(LoginDTO login);
}
