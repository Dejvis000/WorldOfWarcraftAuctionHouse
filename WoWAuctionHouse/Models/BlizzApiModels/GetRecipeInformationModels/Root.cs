using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetRecipeInformationModels
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

    public class Media
    {
        public Key key { get; set; }
        public int id { get; set; }
    }

    public class CraftedItem
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class AllianceCraftedItem
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    } 
    public class HordeCraftedItem
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }


    public class Reagent2
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Reagent
    {
        public Reagent2 reagent { get; set; }
        public int quantity { get; set; }
    }

    public class CraftedQuantity
    {
        public decimal minimum { get; set; }
        public decimal maximum { get; set; }
    }
    public class SlotType
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class ModifiedCraftingSlot
    {
        public SlotType slot_type { get; set; }
        public int display_order { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Media media { get; set; }
        public CraftedItem crafted_item { get; set; }
        public AllianceCraftedItem alliance_crafted_item { get; set; }
        public HordeCraftedItem horde_crafted_item { get; set; }
        public List<Reagent> reagents { get; set; }
        public CraftedQuantity crafted_quantity { get; set; }
        public List<ModifiedCraftingSlot> modified_crafting_slots { get; set; }
    }

}
