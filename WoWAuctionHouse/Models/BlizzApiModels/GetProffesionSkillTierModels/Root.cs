using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetProffesionSkillTierModels
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Type
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Key
    {
        public string href { get; set; }
    }

    public class Media
    {
        public Key key { get; set; }
        public int id { get; set; }
    }

    public class SkillTier
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Type type { get; set; }
        public Media media { get; set; }
        public List<SkillTier> skill_tiers { get; set; }
    }
}
