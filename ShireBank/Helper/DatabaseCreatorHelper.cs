using Microsoft.Extensions.DependencyInjection;
using Repository.Core;
using ShireBank.Services.Builders;

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