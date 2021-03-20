using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using WoWAuctionHouse.Extensions;
using WoWAuctionHouse.Services.BlizzApiService;

namespace WoWAuctionHouse.Services.AuctionService
{
    public class AuctionService : IAuctionService
    {
        private IBlizzApiService _blizzApiService;

        public AuctionService(IBlizzApiService blizzApiService)
        {
            _blizzApiService = blizzApiService;
            auctions = new ObservableCollection<AuctionItemModel>();
        }

        public ObservableCollection<AuctionItemModel> auctions { get; set; }

        private DispatcherTimer Timer;


        public async Task GetAuctionsByItemId(int itemId)
        {
            var itemAuctions = auctions.Where(x => x.ItemId == itemId).ToList();

            foreach (var i in itemAuctions)
            {
                 if (i.BuyOutPrice != null)
                {
                    var toLong = (long)i.BuyOutPrice;
                    var g = toLong.ToGold();
                }

                if (i.UnitPrice > 0)
                {
                    var g = i.UnitPrice.ToGold();
                }
            }
        }
        public async Task GetAuctions()
        {
            AuctionTick(null, null);
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            Timer.Tick += AuctionTick;
            Timer.Start();
        }

        private async void AuctionTick(object sender, EventArgs e)
        {
            auctions = new ObservableCollection<AuctionItemModel>();
            var auc = await _blizzApiService.GetAuctions();

            foreach (var a in auc.auctions)
            {
                auctions.Add(new AuctionItemModel
                {
                    BuyOutPrice = a.buyout,
                    ItemId = a.item.id,
                    Quantity = a.quantity,
                    UnitPrice = a.unit_price
                });
            }
        }
    }

    public class AuctionItemModel : ObservableObject
    {
        public long? BuyOutPrice { get; set; }
        public long UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
    }
}
