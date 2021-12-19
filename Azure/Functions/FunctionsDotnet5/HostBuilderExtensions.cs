using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace FunctionsDotnet5
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureFunctionsAppConfiguration(this IHostBuilder hostBuilder)
            => hostBuilder
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                });
    }
}
