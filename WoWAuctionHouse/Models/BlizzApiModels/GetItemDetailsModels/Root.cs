using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetItemDetailsModels
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Quality
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

    public class ItemClass
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class ItemSubclass
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class InventoryType
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Item
    {
        public Key key { get; set; }
        public int id { get; set; }
    }

    public class Spell2
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Spell
    {
        public Spell2 spell { get; set; }
        public string description { get; set; }
    }

    public class DisplayStrings
    {
        public string header { get; set; }
        public string gold { get; set; }
        public string silver { get; set; }
        public string copper { get; set; }
    }

    public class SellPrice
    {
        public int value { get; set; }
        public DisplayStrings display_strings { get; set; }
    }

    public class PreviewItem
    {
        public Item item { get; set; }
        public Quality quality { get; set; }
        public string name { get; set; }
        public Media media { get; set; }
        public ItemClass item_class { get; set; }
        public ItemSubclass item_subclass { get; set; }
        public InventoryType inventory_type { get; set; }
        public List<Spell> spells { get; set; }
        public SellPrice sell_price { get; set; }
        public bool is_subclass_hidden { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Quality quality { get; set; }
        public int level { get; set; }
        public int required_level { get; set; }
        public Media media { get; set; }
        public ItemClass item_class { get; set; }
        public ItemSubclass item_subclass { get; set; }
        public InventoryType inventory_type { get; set; }
        public int purchase_price { get; set; }
        public int sell_price { get; set; }
        public int max_count { get; set; }
        public bool is_equippable { get; set; }
        public bool is_stackable { get; set; }
        public string description { get; set; }
        public PreviewItem preview_item { get; set; }
        public int purchase_quantity { get; set; }
    }

}
