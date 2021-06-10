using System;
using Repository.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Services.Interfaces.Factories;
using Services.Services.Interfaces.Fasades;

namespace Services.Services.Implementations.Factories
{
    public sealed class DbServicesFactory : IDbServicesFactory
    {
        public IAccountService AccountService => _serviceProvider.GetService<IAccountService>();

        public IAccountOperationService AccountOperationService => _serviceProvider.GetService<IAccountOperationService>();

        public IAccountHistoryFasade AccountHistoryFasade => _serviceProvider.GetService<IAccountHistoryFasade>();

        private readonly IServiceProvider _serviceProvider;

        public DbServicesFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}