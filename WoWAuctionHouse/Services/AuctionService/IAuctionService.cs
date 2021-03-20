using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WoWAuctionHouse.Services.AuctionService
{
    public interface IAuctionService
    {
        Task GetAuctions();
        ObservableCollection<AuctionItemModel> Auctions { get; set; }
        AuctionItemModel GetAuctionsByItemId(int itemId);
    }
}
