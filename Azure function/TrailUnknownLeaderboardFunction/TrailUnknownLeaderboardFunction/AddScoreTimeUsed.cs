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
    public static class AddScoreTimeUsed
    {
        [FunctionName("AddScoreTimeUsed")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("leaderboardcontainer/leaderboardtimeused.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] string leaderboardBlobString,
            [Blob("leaderboardcontainer/leaderboardtimeused.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] TextWriter leaderboardBlobTextWriter,
            ILogger log)
        {
            log.LogInformation("AddScore");

            LeaderboardTimeUsed leaderboardTimeUsed = JsonConvert.DeserializeObject<LeaderboardTimeUsed>(leaderboardBlobString);

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            LeaderboardTrailUnknownTimeUsed leaderboardTrailUnknownTimeUsed = JsonConvert.DeserializeObject<LeaderboardTrailUnknownTimeUsed>(requestBody);

            leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList.Add(leaderboardTrailUnknownTimeUsed);

            string saveBlobData = JsonConvert.SerializeObject(leaderboardTimeUsed);

            leaderboardBlobTextWriter.Write(saveBlobData);

            return new OkObjectResult(saveBlobData);
        }
    }
}
