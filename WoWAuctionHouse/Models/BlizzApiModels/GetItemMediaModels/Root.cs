using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetItemMediaModels
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Asset
    {
        public string key { get; set; }
        public string value { get; set; }
        public int file_data_id { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public List<Asset> assets { get; set; }
        public int id { get; set; }
    }
}
