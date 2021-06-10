using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Repository.Core;
using Repository.Services.Implementations;
using Repository.Services.Interfaces;
using Services.Services.Implementations.Factories;
using Services.Services.Implementations.Fasades;
using Services.Services.Implementations.Services;
using Services.Services.Interfaces.Factories;
using Services.Services.Interfaces.Fasades;
using Services.Services.Interfaces.Services;
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
            }, ServiceLifetime.Transient);

            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IAccountOperationService, AccountOperationService>();
            serviceCollection.AddTransient<IAccountHistoryService, AccountHistoryService>();

            return serviceCollection;
        }

        public static IServiceCollection AddGrpcServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CustomerInterfaceBase, CustomerInterfaceService>();
            serviceCollection.AddTransient<InspectorInterfaceBase, InspectorInterfaceService>();

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
            serviceCollection.AddTransient<IAccountHistoryFasade, AccountHistoryFasade>();

            return serviceCollection;
        }

        public static IServiceCollection AddFactories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDbServicesFactory, DbServicesFactory>();

            return serviceCollection;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAccountHistoryFormaterService, AccountHistoryFormaterService>();
            serviceCollection.AddSingleton<IInspectorBlockerService, InspectorBlockerService>();
            serviceCollection.AddSingleton<IMonitorActivityService, MonitorActivityService>();
            serviceCollection.AddSingleton<IInspectorFasade, InspectorFasade>();

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