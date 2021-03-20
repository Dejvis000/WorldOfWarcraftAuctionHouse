using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WoWAuctionHouse.Services.AuctionService
{
    public interface IAuctionService
    {
        Task GetAuctions();
        ObservableCollection<AuctionItemModel> auctions { get; set; }
        Task GetAuctionsByItemId(int itemId);
    }
}
