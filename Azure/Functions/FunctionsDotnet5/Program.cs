using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsDotnet5
{
    class Program
    {
        public static Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureFunctionsAppConfiguration() // サンプルで独自拡張
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;
                    services.AddHttpClient();
                })
                .Build();
            return host.RunAsync();
        }
    }
}
