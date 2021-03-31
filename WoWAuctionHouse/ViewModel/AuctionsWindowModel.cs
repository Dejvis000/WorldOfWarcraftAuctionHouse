using AutoMapper;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWAuctionHouse.Extensions;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.View;

namespace WoWAuctionHouse.ViewModel
{
    public class AuctionsWindowModel : ViewModelBase
    {
        private ItemModel _item;
        private IAuctionService _auctionService;
        private IMapper _mapper;

        public AuctionsWindowModel(IAuctionService auctionService, IMapper mapper)
        {
            _auctionService = auctionService;
            _mapper = mapper;
        }

        public AuctionsWindow AuctionsWindow { get; set; }

        public ItemModel SelectedItem { get; set; }


        private ObservableCollection<AuctionItemWithGoldModel> _auctions;
        public ObservableCollection<AuctionItemWithGoldModel> Auctions
        {
            get => _auctions;
            set
            {
                Set(() => Auctions, ref _auctions, value);
            }
        }

        public ItemModel Item
        {
            get => _item;
            set
            {
                Set(() => Item, ref _item, value);
                if (Item != null)
                    Auctions = new ObservableCollection<AuctionItemWithGoldModel>(_mapper.Map<List<AuctionItemWithGoldModel>>(_auctionService.GetAuctionsByItemId(Item.Id)));
            }
        }
    }

    public class AuctionItemWithGoldModel
    {
        private long unitPrice;
        private long? buyOutPrice;
        public long? BuyOutPrice
        {
            get => buyOutPrice;
            set
            {
                buyOutPrice = value;
                BuyOutPriceGold = buyOutPrice.ToGold();
            }
        }
        public long UnitPrice
        {
            get => unitPrice;
            set
            {
                unitPrice = value;
                UnitPriceGold = unitPrice.ToGold();
            }
        }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public GoldModel BuyOutPriceGold { get; set; }
        public GoldModel UnitPriceGold { get; set; }
    }
}
