using System.Collections.Generic;

namespace Corr.Starwars.api.Models
{
    public class FilmData
    {
        public int id { get; set; }
        public string producer { get; set; }
        public string title { get; set; }
        public int episode_id { get; set; }
        public string director { get; set; }

        public List<int>people {get; set;}
        public string release_date { get; set; }
        public string opening_crawl { get; set; }
    }

    public class FilmRoot
    {
        public List<FilmData> FilmData { get; set; }
    }
}