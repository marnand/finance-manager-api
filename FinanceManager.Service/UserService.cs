using FinanceManager.Model;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Interfaces;

namespace FinanceManager.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<int> Create(User user)
    {
        return await _userRepository.Create(user);
    }

    public async Task<User> Edit()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> Get(int id)
    {
        return await _userRepository.Get(id);
    }

    public async Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}
