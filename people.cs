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
    public static class people
    {
       [FunctionName("people")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Function started.");

            string id = req.Query["id"];

            // read app settings from ENV vars
            var peopleUrl = Environment.GetEnvironmentVariable("PEOPLE_DATAFILE_URL");
            
            var test = await new BlobClient(new Uri(peopleUrl)).DownloadContentAsync();

            var peopleRoot = JsonConvert.DeserializeObject<PeopleRoot>(test.Value.Content.ToString());

            if (!string.IsNullOrEmpty(id))
            {
                log.LogInformation($"Searching for people data with id '{id}'.");
                var results = peopleRoot.PeopleData.Where(pd => pd.id == int.Parse(id));
                return new OkObjectResult(results);
            }
            else
            {
                log.LogInformation($"Returning list of all names and some metadata.");
                var results = peopleRoot.PeopleData.Select(pd => new {id= pd.id, name = pd.name, homeworld = pd.homeworld, url = generateUrl(req, pd.id)});
                return new OkObjectResult(results);
            }
        }

        private static string generateUrl(HttpRequest request, int id)
        {
            return $"{(request.IsHttps?"https://":"http://")}{request.Host}{request.Path}?id={id}";
        }
    }
}
