using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Repository.Core;
using Repository.Services.Implementations;
using Repository.Services.Interfaces;
using Services.Services.Implementations;
using Services.Services.Interfaces;
using ShireBank.Services.Implementations;
using ShireBank.Services.Interfaces;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

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

            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IAccountOperationService, AccountOperationService>();
            serviceCollection.AddTransient<IAccountHistoryService, AccountHistoryService>();

            return serviceCollection;
        }

        public static IServiceCollection AddGrpcServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CustomerInterfaceBase, CustomerServiceHost>();
            serviceCollection.AddTransient<InspectorInterfaceBase, InspectorServiceHost>();

            serviceCollection.AddTransient<IServerRunnerService, ServerRunnerService>();

            return serviceCollection;
        }

        public static IServiceCollection AddLoggers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILoggerService, LoggerService>();

            return serviceCollection;
        }

        public static IServiceCollection AddFasades(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAccountHistoryFasade, AccountHistoryFasade>();

            return serviceCollection;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAccountHistoryFormaterService, AccountHistoryFormaterService>();

            return serviceCollection;
        }

        public static void InitializeNlog(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(configuration);
            });
        }
    }
}