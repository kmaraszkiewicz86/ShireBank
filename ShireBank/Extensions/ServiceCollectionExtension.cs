using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Core;
using Repository.Services.Implementations;
using Repository.Services.Interfaces;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ShireBankDbContext>(options =>
            {
                options.UseSqlite("FileName=ShrireBankDatabase.db", options =>
                 {
                     options.MigrationsAssembly(Assembly.GetAssembly(typeof(ShireBankDbContext)).FullName);
                 });
            });

            serviceCollection.AddScoped<IAccountService, AccountService>();
            serviceCollection.AddScoped<IAccountOperationService, AccountOperationService>();
            serviceCollection.AddScoped<IAccountHistoryService, AccountHistoryService>();

            return serviceCollection;
        }

        public static IServiceCollection AddGrpcServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<CustomerInterfaceBase, CustomerServiceHost>();

            return serviceCollection;
        }
    }
}
