using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WoWAuctionHouse.Services.BlizzApiService;

namespace WoWAuctionHouse.Services.AuctionService
{
    public class AuctionService : IAuctionService
    {
        private readonly IBlizzApiService _blizzApiService;

        public AuctionService(IBlizzApiService blizzApiService)
        {
            _blizzApiService = blizzApiService;
            Auctions = new ObservableCollection<AuctionItemModel>();
        }

        public ObservableCollection<AuctionItemModel> Auctions { get; set; }


        public List<AuctionItemModel> GetAuctionsByItemId(int itemId)
        {
            var firstItem = Auctions.Where(x => x.ItemId == itemId).First();
            List<List<AuctionItemModel>> groupedAuctions = new List<List<AuctionItemModel>>();
            if (firstItem.BuyOutPrice != null)
            {
                if (firstItem.BuyOutPrice < firstItem.UnitPrice)
                    groupedAuctions = Auctions.Where(x => x.ItemId == itemId).GroupBy(x => x.UnitPrice).Select(x => x.ToList()).ToList();
                else
                    groupedAuctions = Auctions.Where(x => x.ItemId == itemId).GroupBy(x => x.BuyOutPrice).Select(x => x.ToList()).ToList();
            }
            else
                groupedAuctions = Auctions.Where(x => x.ItemId == itemId).GroupBy(x => x.UnitPrice).Select(x => x.ToList()).ToList();
            List<AuctionItemModel> auctions = new List<AuctionItemModel>();
            foreach (var items in groupedAuctions)
            {
                var quantity = 0;
                foreach (var item in items)
                    quantity = item.Quantity;
                auctions.Add(new AuctionItemModel
                {
                    BuyOutPrice = items.First().BuyOutPrice,
                    Quantity = quantity,
                    ItemId = items.First().ItemId,
                    UnitPrice = items.First().UnitPrice
                });
            }
            return auctions;
        }

        public AuctionItemModel GetAuctionByItemId(int itemId)
        {
            var itemAuctions = Auctions.Where(x => x.ItemId == itemId).ToList();
            if (itemAuctions.Any())
            {
                var bestPriceItem = itemAuctions.First();

                foreach (var item in itemAuctions)
                {
                    if (item.BuyOutPrice != null)
                    {
                        if (item.BuyOutPrice < bestPriceItem.BuyOutPrice)
                            bestPriceItem = item;
                    }
                    else
                    {
                        if (item.UnitPrice > 0)
                            if (item.UnitPrice < bestPriceItem.UnitPrice)
                                bestPriceItem = item;
                    }
                }
                return bestPriceItem;
            }
            return new AuctionItemModel();
        }
        public async Task GetAuctions()
        {
            Auctions = new ObservableCollection<AuctionItemModel>();
            var auc = await _blizzApiService.GetAuctions();
            while (auc == null)
            {
                await Task.Delay(500);
                auc = await _blizzApiService.GetAuctions();
            }
            foreach (var a in auc.auctions)
            {
                Auctions.Add(new AuctionItemModel
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
