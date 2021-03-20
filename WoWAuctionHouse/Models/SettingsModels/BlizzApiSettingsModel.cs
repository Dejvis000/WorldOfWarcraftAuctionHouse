namespace WoWAuctionHouse.Models.SettingsModels
{
    public class BlizzApiSettingsModel
    {
        public string Region { get; set; }
        public string Realm { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DynamicNamespace { get; set; }
        public string StaticNamespace { get; set; }
        public string Locale { get; set; }
    }
}
