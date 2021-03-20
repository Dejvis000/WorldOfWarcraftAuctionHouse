using WoWAuctionHouse.Models.SettingsModels;

namespace WoWAuctionHouse.Services.SettingsService
{
    public interface ISettingsService
    {
        BlizzApiSettingsModel BlizzApiSettings { get; }
    }
}
