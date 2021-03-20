using System.Collections.Generic;

namespace WoWAuctionHouse.Models.BlizzApiModels.GetProffesionRecipesModels
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

    public class Recipe
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public List<Recipe> recipes { get; set; }
    }

    public class Root
    {
        public Links _links { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int minimum_skill_level { get; set; }
        public int maximum_skill_level { get; set; }
        public List<Category> categories { get; set; }
    }

}
