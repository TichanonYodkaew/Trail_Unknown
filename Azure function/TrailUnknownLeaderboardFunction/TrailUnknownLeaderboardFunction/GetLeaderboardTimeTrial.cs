using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace TrailUnknownLeaderboardFunction
{
    public static class GetLeaderboardTimeTrial
    {
        [FunctionName("GetLeaderboardTimeTrial")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Blob("leaderboardcontainer/leaderboardtimetrial.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] string leaderboardBlobString,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(leaderboardBlobString);
        }
    }
}
