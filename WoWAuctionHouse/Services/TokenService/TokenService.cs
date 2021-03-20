using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using WoWAuctionHouse.Models.SessionModels;
using WoWAuctionHouse.Services.BlizzApiService;
using WoWAuctionHouse.Services.SettingsService;

namespace WoWAuctionHouse.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private ISettingsService _settingsService;

        public TokenService(ISettingsService settingsService )
        {
            _settingsService = settingsService;             
        }

        public async Task<TokenModel> GetToken()
        {
            var clientId = _settingsService.BlizzApiSettings.ClientId;
            var clientSecret = _settingsService.BlizzApiSettings.ClientSecret;

            var client = new RestClient("https://eu.battle.net/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}", ParameterType.RequestBody);
            var resp = await client.ExecuteAsync(request);
            var blizzModel = JsonConvert.DeserializeObject<BlizzTokenModel>(resp.Content);

            return new TokenModel
            {
                AccessToken = blizzModel.access_token,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Expiration = DateTime.Now.AddSeconds(blizzModel.expires_in),
                TokenType = blizzModel.token_type,
            };
        }
    }
}
