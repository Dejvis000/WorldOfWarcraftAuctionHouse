using WoWAuctionHouse.Models.SettingsModels;

namespace WoWAuctionHouse.Services.SettingsService
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(BlizzApiSettingsModel blizzApiSettings)
        {
            BlizzApiSettings = blizzApiSettings;
        }

        public BlizzApiSettingsModel BlizzApiSettings { get; }
    }
}
