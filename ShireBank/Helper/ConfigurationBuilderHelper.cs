using Microsoft.Extensions.Configuration;

namespace ShireBank.Helper
{
    public static class ConfigurationBuilderHelper
    {
        public static IConfiguration GetNLogConfig()
        {
            return new ConfigurationBuilder()
             .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .Build();
        }
    }
}