using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace FunctionsDotnet5
{
    class Program
    {
        public static Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults() // 必須
                // 構成情報を追加する場合
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                })
                // 依存関係を注入する場合
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;
                    services.AddHttpClient(); // 例としてHttpClientをDI
                })
                .Build();
            return host.RunAsync();
        }
    }
}
