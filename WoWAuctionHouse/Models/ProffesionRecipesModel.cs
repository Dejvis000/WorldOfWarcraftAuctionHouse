using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace WoWAuctionHouse.Models
{
    public class ProffesionRecipesModel
    {
        public string Name { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
    }

    public class RecipeCategoriesModel : ObservableObject
    {
        public string Name { get; set; }
        private ObservableCollection<RecipeModel> _recipes;
        public ObservableCollection<RecipeModel> Recipes
        {
            get
            {
                return _recipes;
            }

            set
            {
                Set(() => this.Recipes, ref _recipes, value);
            }
        }
    }

    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ItemMediaURL { get; set; }
    }
}
