using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Repository.Core;

namespace ShireBank.Helper
{
    public static class DatabaseCreatorHelper
    {
        public static async Task CreateDatabaseAsync(this IServiceProvider serviceProvider)
        {
            await serviceProvider.GetService<ShireBankDbContext>().CreateDatabaseIfNotExistsAsync();
        }
    }
}