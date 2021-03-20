using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetProffesionModels
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Key
    {
        public string href { get; set; }
    }

    public class Profession
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public List<Profession> professions { get; set; }
    }


}
