using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Corr.Starwars.api.Models;
using Azure.Storage.Blobs;
using System.Linq;

namespace Corr.Starwars.api
{
     public static class planets
    {
        [FunctionName("planets")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Function started.");

            string id = req.Query["id"];

            // read app settings from ENV vars
            var planetUrl = Environment.GetEnvironmentVariable("PLANETS_DATAFILE_URL");
            
            var test = await new BlobClient(new Uri(planetUrl)).DownloadContentAsync();

            var planetRoot = JsonConvert.DeserializeObject<PlanetRoot>(test.Value.Content.ToString());

            if (!string.IsNullOrEmpty(id))
            {
                log.LogInformation($"Searching for planet data with id '{id}'.");
                var results = planetRoot.PlanetData.Where(pd => pd.id == int.Parse(id));
                return new OkObjectResult(results);
            }
            else
            {
                log.LogInformation($"Returning list of all planets and some metadata.");
                var results = planetRoot.PlanetData.Select(pd => new {id= pd.id, name = pd.name, climate = pd.climate, url = generateUrl(req, pd.id)});
                return new OkObjectResult(results);
            }
        }

        private static string generateUrl(HttpRequest request, int id)
        {
            return $"{(request.IsHttps?"https://":"http://")}{request.Host}{request.Path}?id={id}";
        }
    }
}
