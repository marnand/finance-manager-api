using FinanceManager.Config;
using FinanceManager.Repository;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Service.Extensions;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseConnectionFactory>();

        services.AddScoped<JWTToken>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
