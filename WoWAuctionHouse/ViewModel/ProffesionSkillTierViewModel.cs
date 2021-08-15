using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WoWAuctionHouse.Infrastructure;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.Services.BlizzApiService;
using WoWAuctionHouse.Services.ExpansionsService;
using WoWAuctionHouse.Services.FilesService;

namespace WoWAuctionHouse.ViewModel
{
    public class ProffesionSkillTierViewModel : ViewModelBase
    {
        private ProffesionModel _proffesion;

        private readonly IFrameNavigationService _navigationService;
        private readonly IBlizzApiService _blizzApiService;
        private readonly IExpansionsService _expansionsService;
        private readonly IFilesService _filesService;

        private ObservableCollection<ProffesionTierModel> _proffesionTierCollection;
        private IEnumerable<ProffesionTierModel> SelectedProffesionTier
        {
            get
            {
                return ProffesionTierCollection.Where(x => x.IsSelected);
            }
        }

        private readonly IAuctionService _auctionService;
        public ProffesionSkillTierViewModel(IFrameNavigationService navigationService, IBlizzApiService blizzApiService, 
            IAuctionService auctionService, IExpansionsService expansionsService, IFilesService filesService)
        {
            _navigationService = navigationService;
            _blizzApiService = blizzApiService;
            _auctionService = auctionService;
            _expansionsService = expansionsService;
            _filesService = filesService;
            ConfigureCommands();
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand SelectTierCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public ObservableCollection<ProffesionTierModel> ProffesionTierCollection
        {
            get
            {
                return _proffesionTierCollection;
            }

            set
            {
                Set(() => this.ProffesionTierCollection, ref _proffesionTierCollection, value);
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

        private void ConfigureCommands()
        {
            OnLoadCommand = new RelayCommand(async () =>
            {
                _auctionService.Auctions.CollectionChanged += Auctions_CollectionChanged;
                AuctionCount = _auctionService.Auctions.Count;
                _proffesion = (ProffesionModel)_navigationService.Parameter;

                await InitProffesionTier();
            });
            SelectTierCommand = new RelayCommand(() =>
            {
                var tier = SelectedProffesionTier.FirstOrDefault();

                _navigationService.NavigateTo("ProffesionRecipesView", new ProffesionsWithTiersModel
                {
                    Proffesion = _proffesion,
                    Tiers = tier
                });
            });
            BackCommand = new RelayCommand(() => _navigationService.NavigateTo("ProffesionView"));
        }

        private async Task InitProffesionTier()
        {
            var tiers = await _blizzApiService.GetProffesionSkillTires(_proffesion.Id);
            ProffesionTierCollection = new ObservableCollection<ProffesionTierModel>();
            foreach (var item in tiers.skill_tiers)
            {
                var expansion = await _filesService.GetExpansionImage(item.name);
                ProffesionTierCollection.Add(new ProffesionTierModel
                {
                    Id = item.id,
                    Name = item.name,
                    IsSelected = false,
                    ImageURL = expansion.IconUrl,
                    Order = expansion.Number
                });
            }
            ProffesionTierCollection = new ObservableCollection<ProffesionTierModel>(ProffesionTierCollection.OrderBy(x => x.Order));
        }
        private void Auctions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AuctionCount = _auctionService.Auctions.Count;
        }

    }
}
