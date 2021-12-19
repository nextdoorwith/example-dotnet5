using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace FunctionsDotnet5
{
    public static class Function1
    {
        [Function("Function1")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(Function1));
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queries = HttpUtility.ParseQueryString(req.Url.Query);
            string name = queries["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Body.Write(Encoding.Default.GetBytes(responseMessage));
            return response;
        }
    }
}
