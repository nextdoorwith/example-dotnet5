using System;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;

namespace FunctionsDotnet5
{
    public class Function2
    {
        private IConfiguration _configuration;
        public Function2(IConfiguration configuration)
            => _configuration = configuration;

        [Function("Function2")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(Function1));
            log.LogInformation($"key1={_configuration["key1"]}");
            log.LogInformation($"key2={_configuration["key2"]}");
            log.LogInformation($"key3={_configuration["key3"]}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}
