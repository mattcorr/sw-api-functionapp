using System.Collections.Generic;

namespace Corr.Starwars.api.Models
{
   public class PlanetData
    {
        public string diameter { get; set; }
        public string rotation_period { get; set; }
        public string orbital_period { get; set; }
        public string gravity { get; set; }
        public string population { get; set; }
        public string climate { get; set; }
        public string terrain { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class PlanetRoot
    {
        public List<PlanetData> PlanetData { get; set; }
    }
}