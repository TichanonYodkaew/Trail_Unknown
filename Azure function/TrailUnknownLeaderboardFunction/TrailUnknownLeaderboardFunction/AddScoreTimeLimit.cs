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
    public static class AddScoreTimeLimit
    {
        [FunctionName("AddScoreTimeLimit")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("leaderboardcontainer/leaderboardtimelimit.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] string leaderboardBlobString,
            [Blob("leaderboardcontainer/leaderboardtimelimit.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] TextWriter leaderboardBlobTextWriter,
            ILogger log)
        {
            log.LogInformation("AddScore");

            LeaderboardTimeLimit leaderboardTimeLimit = JsonConvert.DeserializeObject<LeaderboardTimeLimit>(leaderboardBlobString);

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            LeaderboardTrailUnknownTimeLimit leaderboardTrailUnknownTimeLimit = JsonConvert.DeserializeObject<LeaderboardTrailUnknownTimeLimit>(requestBody);

            leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList.Add(leaderboardTrailUnknownTimeLimit);

            string saveBlobData = JsonConvert.SerializeObject(leaderboardTimeLimit);

            leaderboardBlobTextWriter.Write(saveBlobData);

            return new OkObjectResult(saveBlobData);
        }
    }
}
