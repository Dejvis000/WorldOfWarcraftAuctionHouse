using WoWAuctionHouse.Models;

namespace WoWAuctionHouse.Services.ExpansionsService
{
    public interface IExpansionsService
    {
        ExpansionModel GetExpansionByKey(string key);
    }
}
