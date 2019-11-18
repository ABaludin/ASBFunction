using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ABaludin.Asb
{
    public static class ASB
    {
        [FunctionName("ASB")]
        public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
      ILogger log,
      [ServiceBus("nav2asb", Connection = "ServiceBusConnection", EntityType = Microsoft.Azure.WebJobs.ServiceBus.EntityType.Queue)] out string busMessage)
        {
        string message = new StreamReader(req.Body).ReadToEnd();
        
        busMessage = message;
        
        return message != null
            ? (ActionResult) new OkObjectResult("ok")
            : new BadRequestObjectResult("No message");
        }
    }
}
