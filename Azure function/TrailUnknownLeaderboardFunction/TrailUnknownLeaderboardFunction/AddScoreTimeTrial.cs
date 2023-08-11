using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace TrailUnknownLeaderboardFunction
{
    public static class AddScoreTimeTrial
    {
        [FunctionName("AddScoreTimeTrial")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("leaderboardcontainer/leaderboardtimetrial.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] string leaderboardBlobString,
            [Blob("leaderboardcontainer/leaderboardtimetrial.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] TextWriter leaderboardBlobTextWriter,
            ILogger log)
        {
            log.LogInformation("AddScore");

            LeaderboardTimeTrial leaderboardTimeTrial = JsonConvert.DeserializeObject<LeaderboardTimeTrial>(leaderboardBlobString);

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            LeaderboardTrailUnknownTimeTrial leaderboardTrailUnknownTimeTrial = JsonConvert.DeserializeObject<LeaderboardTrailUnknownTimeTrial>(requestBody);

            leaderboardTimeTrial.leaderboardTrailUnknownTimeTrialList.Add(leaderboardTrailUnknownTimeTrial);

            string saveBlobData = JsonConvert.SerializeObject(leaderboardTimeTrial);

            leaderboardBlobTextWriter.Write(saveBlobData);

            return new OkObjectResult(saveBlobData);
        }
    }
}
