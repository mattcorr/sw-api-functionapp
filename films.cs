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
    public static class films
    {
        [FunctionName("films")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Function started.");

            // read request parameters
            string id = req.Query["id"];
            
            // read app settings from ENV vars
            var filmsUrl = Environment.GetEnvironmentVariable("FILMS_DATAFILE_URL");
            
            var test = await new BlobClient(new Uri(filmsUrl)).DownloadContentAsync();

            var filmRoot = JsonConvert.DeserializeObject<FilmRoot>(test.Value.Content.ToString());

            if (!string.IsNullOrEmpty(id))
            {
                log.LogInformation($"Searching for film data with id '{id}'.");
                var results = filmRoot.FilmData.Where(fd => fd.id == int.Parse(id));
                return new OkObjectResult(results);
            }
            else
            {
                log.LogInformation($"Returning list of all films with some metadata");
                var results = filmRoot.FilmData.Select(fd => new {id= fd.id, title = fd.title, director = fd.director, url = generateUrl(req, fd.id)});
                return new OkObjectResult(results);
            }
        }

        private static string generateUrl(HttpRequest request, int id)
        {
            return $"{(request.IsHttps?"https://":"http://")}{request.Host}{request.Path}?id={id}";
        }
    }
}