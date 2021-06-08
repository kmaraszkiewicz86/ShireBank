using Microsoft.Extensions.DependencyInjection;
using Repository.Core;
using ShireBank.Services.Implementations.Builders;

namespace ShireBank.Helper
{
    public static class DatabaseCreatorHelper
    {
        public static void CreateDatabase()
        {
            HostingBuilder.ServiceProvider.GetService<ShireBankDbContext>().CreateDatabaseIfNotExists();
        }
    }
}