using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.View;

namespace WoWAuctionHouse.ViewModel
{
    public class AuctionsWindowModel : ViewModelBase
    {
        private int _itemId;
        private IAuctionService _auctionService;

        public AuctionsWindowModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public AuctionsWindow AuctionsWindow { get; set; }

        public ItemModel SelectedItem { get; set; }


        private ObservableCollection<AuctionItemModel> _auctions;
        public ObservableCollection<AuctionItemModel> Auctions
        {
            get => _auctions;
            set
            {
                Set(() => Auctions, ref _auctions, value);
            }
        }

        public int ItemId
        {
            get => _itemId;
            set
            {
                Set(() => ItemId, ref _itemId, value);
                Auctions = new ObservableCollection<AuctionItemModel>(_auctionService.GetAuctionsByItemId(ItemId));
            }
        }
    }
}
