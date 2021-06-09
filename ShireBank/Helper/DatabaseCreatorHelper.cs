using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Repository.Core;
using ShireBank.Services.Implementations.Builders;

namespace ShireBank.Helper
{
    public static class DatabaseCreatorHelper
    {
        public static async Task CreateDatabaseAsync()
        {
            await HostingBuilder.ServiceProvider.GetService<ShireBankDbContext>().CreateDatabaseIfNotExistsAsync();
        }
    }
}