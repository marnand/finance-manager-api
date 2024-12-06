using FinanceManager.Model;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Interfaces;

namespace FinanceManager.Service;

internal class AccountService(IAccountRepository accountRepository) : IAccountService
{
    private readonly IAccountRepository _acctionRepository = accountRepository;

    public async Task Create(Account user)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> Edit(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Account>> Get(int id = 0)
    {
        return await _acctionRepository.Get(id);
    }

    public async Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}
