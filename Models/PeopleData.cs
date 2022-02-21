using System.Collections.Generic;

namespace Corr.Starwars.api.Models
{
    public class PeopleData
    {
        public string height { get; set; }
        public string mass { get; set; }
        public string hair_color { get; set; }
        public string skin_color { get; set; }
        public string eye_color { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public int homeworld { get; set; }
        public int id { get; set; }
    }

    public class PeopleRoot
    {
        public List<PeopleData> PeopleData { get; set; }
    }
}