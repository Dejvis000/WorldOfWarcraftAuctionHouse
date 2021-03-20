using GalaSoft.MvvmLight;
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


        public AuctionItemModel GetAuctionsByItemId(int itemId)
        {
            var itemAuctions = Auctions.Where(x => x.ItemId == itemId).ToList();

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
        public async Task GetAuctions()
        {
            Auctions = new ObservableCollection<AuctionItemModel>();
            var auc = await _blizzApiService.GetAuctions();

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
