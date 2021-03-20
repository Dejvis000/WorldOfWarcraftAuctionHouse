using System;
using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetAuctionsModels
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class ConnectedRealm
    {
        public string href { get; set; }
    }

    public class Modifier
    {
        public int type { get; set; }
        public int value { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int? context { get; set; }
        public List<Modifier> modifiers { get; set; }
    }

    public class Auction
    {
        public int id { get; set; }
        public Item item { get; set; }
        public int quantity { get; set; }
        public Int64 unit_price { get; set; }
        public string time_left { get; set; }
        public Int64? buyout { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public ConnectedRealm connected_realm { get; set; }
        public List<Auction> auctions { get; set; }
    }
}
