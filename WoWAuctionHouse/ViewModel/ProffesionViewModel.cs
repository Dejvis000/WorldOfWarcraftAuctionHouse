using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using WoWAuctionHouse.Infrastructure;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.Services.BlizzApiService;
using WoWAuctionHouse.Services.TokenService;

namespace WoWAuctionHouse.ViewModel
{
    public class ProffesionViewModel : ViewModelBase
    {
        private readonly IFrameNavigationService _navigationService;
        private readonly ITokenService _tokenService;
        private readonly IBlizzApiService _blizzApiService;
        private readonly IAuctionService _auctionService;


        private ObservableCollection<ProffesionModel> _proffesionCollection;
        private IEnumerable<ProffesionModel> SelectedProffesion
        {
            get
            {
                return ProffesionCollection.Where(x => x.IsSelected);
            }
        }
        
        public ProffesionViewModel(IFrameNavigationService navigationService, ITokenService tokenService, IBlizzApiService blizzApiService, IAuctionService auctionService)
        {
            _navigationService = navigationService;
            _tokenService = tokenService;
            _blizzApiService = blizzApiService;
            _auctionService = auctionService;
            ConfigureCommands();
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand SelectProffesionCommand { get; set; }
        
        public ObservableCollection<ProffesionModel> ProffesionCollection
        {
            get => _proffesionCollection;

            set
            {
                Set(() => ProffesionCollection, ref _proffesionCollection, value);
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

        private readonly Timer _auctionTimer = new Timer(600000);

        private void ConfigureCommands()
        {
            OnLoadCommand = new RelayCommand(async () =>
            {
                _auctionTimer.Stop();
                _auctionTimer.AutoReset = true;
                _auctionTimer.Elapsed += _auctionTimer_Elapsed;
                Common.Session.Instance.Token = await _tokenService.GetToken();
                Common.Session.Instance.Token.RealmId = await _blizzApiService.GetRealmId();
                await InitProffesions();
                _auctionTimer_Elapsed(null,null);
                _auctionTimer.Start();
                _auctionService.Auctions.CollectionChanged += Auctions_CollectionChanged;
            });
            SelectProffesionCommand = new RelayCommand(() =>
            {
                if (SelectedProffesion.Any())
                {
                    _navigationService.NavigateTo("ProffesionSkillTierView", SelectedProffesion.First());
                }
            });
        }

        private void _auctionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Factory.StartNew(_auctionService.GetAuctions);
        }

        private void Auctions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AuctionCount = _auctionService.Auctions.Count;
        }

        private async Task InitProffesions()
        {
            var proffesions = await _blizzApiService.GetProffesions();
            ProffesionCollection = new ObservableCollection<ProffesionModel>();

            foreach (var item in proffesions.professions)
            {
                var media = await _blizzApiService.GetProffesionMedia(item.id);
                ProffesionCollection.Add(new ProffesionModel
                {
                    Id = item.id,
                    IsSelected = false,
                    Name = item.name,
                    ImageURL = media.assets.FirstOrDefault()?.value
                });
            }
        }
    }
}
