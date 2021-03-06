using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WoWAuctionHouse.Infrastructure;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.Services.BlizzApiService;

namespace WoWAuctionHouse.ViewModel
{
    public class ProffesionSkillTierViewModel : ViewModelBase
    {
        private ProffesionModel proffesion;

        private IFrameNavigationService _navigationService;
        private IBlizzApiService _blizzApiService;

        private ObservableCollection<ProffesionTierModel> _proffesionTierCollection;
        private IEnumerable<ProffesionTierModel> SelectedProffesionTier
        {
            get
            {
                return ProffesionTierCollection.Where(x => x.IsSelected);
            }
        }


        private IAuctionService _auctionService;
        public ProffesionSkillTierViewModel(IFrameNavigationService navigationService, IBlizzApiService blizzApiService, IAuctionService auctionService)
        {
            _navigationService = navigationService;
            _blizzApiService = blizzApiService;
            _auctionService = auctionService;
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
            get
            {
                return _auctionCount;
            }

            set
            {
                Set(() => AuctionCount, ref _auctionCount, value);
            }
        }

        private void ConfigureCommands()
        {
            OnLoadCommand = new RelayCommand(async () =>
            {
                _auctionService.auctions.CollectionChanged += Auctions_CollectionChanged;
                AuctionCount = _auctionService.auctions.Count;
                proffesion = (ProffesionModel)_navigationService.Parameter;

                await InitProffesionTier();
            });
            SelectTierCommand = new RelayCommand(() =>
            {
                var tier = SelectedProffesionTier.FirstOrDefault();

                _navigationService.NavigateTo("ProffesionRecipesView", new ProffesionsWithTiersModel
                {
                    Proffesion = proffesion,
                    Tiers = tier
                });
            });
            BackCommand = new RelayCommand(() => _navigationService.NavigateTo("ProffesionView"));
        }

        private async Task InitProffesionTier()
        {
            var tiers = await _blizzApiService.GetProffesionSkillTires(proffesion.Id);
            ProffesionTierCollection = new ObservableCollection<ProffesionTierModel>();
            foreach (var item in tiers.skill_tiers)
            {
                ProffesionTierCollection.Add(new ProffesionTierModel
                {
                    Id = item.id,
                    Name = item.name,
                    IsSelected = false
                });
            }
        }
        private void Auctions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AuctionCount = _auctionService.auctions.Count;
        }

    }
}
