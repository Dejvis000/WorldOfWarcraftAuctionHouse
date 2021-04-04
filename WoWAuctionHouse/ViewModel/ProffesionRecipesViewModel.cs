using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WoWAuctionHouse.Extensions;
using WoWAuctionHouse.Infrastructure;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Models.BlizzApiModels.GetItemMediaModels;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.Services.BlizzApiService;
using WoWAuctionHouse.Services.FilesService;
using WoWAuctionHouse.View;
using static WoWAuctionHouse.Behaviors.TreeViewSelectionBehavior;

namespace WoWAuctionHouse.ViewModel
{
    public class ProffesionRecipesViewModel : ViewModelBase
    {
        private readonly IFrameNavigationService _navigationService;
        private readonly IBlizzApiService _blizzApiService;
        private readonly IAuctionService _auctionService;
        private readonly IFilesService _filesService;

        public ProffesionRecipesViewModel(IFrameNavigationService navigationService, IBlizzApiService blizzApiService, 
            IAuctionService auctionService, IFilesService filesService)
        {
            _navigationService = navigationService;
            _blizzApiService = blizzApiService;
            _auctionService = auctionService;
            _filesService = filesService;
            ConfigureCommands();

            ItemSelected += ProffesionRecipesViewModel_ItemSelected;

            ReagentsCollection = new ObservableCollection<ItemModel>();
            ItemCollection = new ObservableCollection<ItemModel>();
        }

        private ProffesionsWithTiersModel _proffesionsWithTiers;
        private ProffesionRecipesModel _proffesionRecipes;

        private List<Models.BlizzApiModels.GetRecipeInformationModels.Root> _allRecipes;

        public IsChildOfPredicate HierarchyPredicate { get; set; }
        private RecipeModel _selectedRecipe;

        private ObservableCollection<ItemModel> _reagentsCollection;
        private ObservableCollection<ItemModel> _itemCollection;

        private event Action ItemSelected;

        private ItemModel _selectedReagent;
        private ItemModel _selectedItem;

        private AuctionsWindow _auctionWindow;

        private async void ProffesionRecipesViewModel_ItemSelected()
        {
            var recipe = _allRecipes.Find(x => x.id == SelectedRecipe.Id);
            ReagentsCollection.Clear();
            ItemCollection.Clear();
            AuctionItemModel auctionItem;
            foreach (var r in recipe.reagents)
            {
                var image = await _filesService.GetItemImage(r.reagent.id); //await _blizzApiService.GetItemMedia(r.reagent.id);
                //while (image?.assets == null)
                //   image = await _blizzApiService.GetItemMedia(r.reagent.id);

                auctionItem = _auctionService.GetAuctionByItemId(r.reagent.id);
                if (auctionItem != null)
                {
                    ReagentsCollection.Add(new ItemModel
                    {
                        Id = r.reagent.id,
                        Name = r.reagent.name,
                        Quantity = r.quantity,
                        ItemImage = image,
                        Gold = auctionItem.BuyOutPrice?.ToGold() ?? auctionItem.UnitPrice.ToGold()
                    });
                }
            }

            var craftedItemId = recipe.crafted_item?.id ?? recipe.horde_crafted_item.id;
            var craftedItemName = recipe.crafted_item != null ? recipe.crafted_item.name : recipe.horde_crafted_item.name;

            var itemImage = await _filesService.GetItemImage(craftedItemId); //await _blizzApiService.GetItemMedia(craftedItemId);
            //while (itemImage?.assets == null)
            //    itemImage = await _blizzApiService.GetItemMedia(craftedItemId);

            auctionItem = _auctionService.GetAuctionByItemId(craftedItemId);

            ItemCollection.Add(new ItemModel
            {
                Id = craftedItemId,
                ItemImage = itemImage,
                Name = craftedItemName,
                Quantity = 1,
                Gold = auctionItem.BuyOutPrice?.ToGold() ?? auctionItem.UnitPrice.ToGold()
            });
        }

        public ProffesionRecipesModel ProffesionRecipes
        {
            get => _proffesionRecipes;

            set
            {
                Set(() => this.ProffesionRecipes, ref _proffesionRecipes, value);
            }
        }

        public RecipeModel SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                Set(() => this.SelectedRecipe, ref _selectedRecipe, value);
                ItemSelected?.Invoke();
            }
        }

        public ObservableCollection<ItemModel> ReagentsCollection
        {
            get => _reagentsCollection;

            set
            {
                Set(() => this.ReagentsCollection, ref _reagentsCollection, value);
            }
        }

        public ObservableCollection<ItemModel> ItemCollection
        {
            get => _itemCollection;

            set
            {
                Set(() => this.ItemCollection, ref _itemCollection, value);
            }
        }

        private ObservableCollection<RecipeCategoriesModel> _recipeCategories;

        public ObservableCollection<RecipeCategoriesModel> RecipeCategoriesCollection
        {
            get => _recipeCategories;

            set
            {
                Set(() => this.RecipeCategoriesCollection, ref _recipeCategories, value);
            }
        }

        public ItemModel SelectedReagent
        {
            get => _selectedReagent;
            set
            {
                Set(() => this.SelectedReagent, ref _selectedReagent, value);
                ShowAuctionWidnows(SelectedReagent);
            }
        }

        public ItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(() => this.SelectedItem, ref _selectedItem, value);
                ShowAuctionWidnows(SelectedItem);
            }
        }

        private void ShowAuctionWidnows(ItemModel model)
        {
            if (model != null)
            {
                _auctionWindow = new AuctionsWindow(model)
                {
                    Topmost = true
                };
                _auctionWindow.Show();
            }
        }

        private int _auctionCount;
        public int AuctionCount
        {
            get => _auctionCount;

            set
            {
                Set(() => AuctionCount, ref _auctionCount, value);
            }
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private void ConfigureCommands()
        {
            OnLoadCommand = new RelayCommand(async () =>
            {
                _auctionService.Auctions.CollectionChanged += Auctions_CollectionChanged;
                AuctionCount = _auctionService.Auctions.Count;
                _proffesionsWithTiers = (ProffesionsWithTiersModel)_navigationService.Parameter;
                _allRecipes = new List<Models.BlizzApiModels.GetRecipeInformationModels.Root>();
                var response = await _blizzApiService.GetProffesionRecipes(_proffesionsWithTiers.Proffesion.Id, _proffesionsWithTiers.Tiers.Id);
                ProffesionRecipes = new ProffesionRecipesModel
                {
                    MaxLevel = response.maximum_skill_level,
                    MinLevel = response.minimum_skill_level,
                    Name = response.name,
                };
                var i = 0;
                RecipeCategoriesCollection = new ObservableCollection<RecipeCategoriesModel>();
                foreach (var cat in response.categories)
                {
                    RecipeCategoriesCollection.Add(new RecipeCategoriesModel
                    {
                        Name = cat.name,
                    });
                    foreach (var res in cat.recipes)
                    {
                        var recipeInfo = await _blizzApiService.GetRecipeInformation(res.id);
                        while (recipeInfo == null)
                            recipeInfo = await _blizzApiService.GetRecipeInformation(res.id);
                        var inBase = IsItemInBase(recipeInfo);

                        string media = "";
                        if (inBase)
                        {
                            _allRecipes.Add(recipeInfo);
                            var retryCount = 0;
                            do
                            {
                                media = await _filesService.GetItemImage(recipeInfo.crafted_item?.id ??
                                                                   recipeInfo.horde_crafted_item.id); //await _blizzApiService.GetItemMedia(recipeInfo.crafted_item?.id ??
                                                                                                      // recipeInfo.horde_crafted_item.id);
                                retryCount++;
                            } while (media == null || retryCount > 10);
                        }
                        var recipeModel = new RecipeModel
                        {
                            Id = res.id,
                            Name = res.name,
                            ItemMediaURL = inBase ? media : ""
                        };
                        if (RecipeCategoriesCollection[i].Recipes == null)
                            RecipeCategoriesCollection[i].Recipes = new ObservableCollection<RecipeModel>();
                        RecipeCategoriesCollection[i].Recipes.Add(recipeModel);
                    }
                    i++;
                }
            });

            BackCommand = new RelayCommand(() => _navigationService.NavigateTo("ProffesionSkillTierView", _proffesionsWithTiers.Proffesion));
        }

        private static bool IsItemInBase(Models.BlizzApiModels.GetRecipeInformationModels.Root item)
        {
            return item.crafted_item != null || item.horde_crafted_item != null || item.alliance_crafted_item != null;
        }

        private void Auctions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AuctionCount = _auctionService.Auctions.Count;
        }
    }
}
