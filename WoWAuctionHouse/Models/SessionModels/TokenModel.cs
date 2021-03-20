using System;

namespace WoWAuctionHouse.Models.SessionModels
{
    public class TokenModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int RealmId { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsTokenExpired() => DateTime.Now > Expiration;
    }
}
