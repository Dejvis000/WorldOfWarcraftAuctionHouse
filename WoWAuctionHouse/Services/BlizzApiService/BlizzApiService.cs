using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using WoWAuctionHouse.Common;
using WoWAuctionHouse.Services.SettingsService;
using WoWAuctionHouse.Services.TokenService;

namespace WoWAuctionHouse.Services.BlizzApiService
{
    public class BlizzApiService : IBlizzApiService
    {
        private ITokenService _tokenService;
        private ISettingsService _settingsService;

        public BlizzApiService(ITokenService tokenService, ISettingsService settingsService)
        {
            _tokenService = tokenService;
            _settingsService = settingsService;
        }

        public async Task<int> GetRealmId()
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var realm = _settingsService.BlizzApiSettings.Realm;
            var blizzNamespace = _settingsService.BlizzApiSettings.DynamicNamespace;
            var token = Session.Instance.Token.AccessToken;
            var requestURL = $"https://eu.api.blizzard.com/data/wow/search/connected-realm?namespace={blizzNamespace}&realms.name.en_US={realm}&access_token={token}";
            var client = new RestClient(requestURL);
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);
            var realmInfo = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetRealmIdModels.Root>(resp.Content);
            return realmInfo.results.FirstOrDefault().data.id;
        }

        //get auctions URL: https://eu.api.blizzard.com/data/wow/connected-realm/1403/auctions?namespace=dynamic-eu&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetAuctionsModels.Root> GetAuctions()
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var realmId = Session.Instance.Token.RealmId;
            var blizzNamespace = _settingsService.BlizzApiSettings.DynamicNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;
            var requestURL = $"https://{region}.api.blizzard.com/data/wow/connected-realm/{realmId}/auctions?namespace={blizzNamespace}&locale={locale}&access_token={token}";

            var client = new RestClient(requestURL);
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);
            var auctions = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetAuctionsModels.Root>(resp.Content);
            return auctions;
        }

        // get proffesions URL: https://eu.api.blizzard.com/data/wow/profession/index?namespace=static-eu&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetProffesionModels.Root> GetProffesions()
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var blizzNamespace = _settingsService.BlizzApiSettings.StaticNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;

            var client = new RestClient($"https://{region}.api.blizzard.com/data/wow/profession/index?namespace={blizzNamespace}&locale={locale}&access_token={token}");
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);

            var proffesions = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetProffesionModels.Root>(resp.Content);
            return proffesions;
        }

        // get proffesion media URL: https://eu.api.blizzard.com/data/wow/media/profession/2759?namespace=static-eu&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetProffesionMediaModels.Root> GetProffesionMedia(int proffesionId)
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var blizzNamespace = _settingsService.BlizzApiSettings.StaticNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;

            var client = new RestClient($"https://{region}.api.blizzard.com/data/wow/media/profession/{proffesionId}?namespace={blizzNamespace}&locale={locale}&access_token={token}");
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);

            var proffesionMedia = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetProffesionMediaModels.Root>(resp.Content);
            return proffesionMedia;
        }

        // get proffesion tiers URL: https://eu.api.blizzard.com/data/wow/profession/197?namespace=static-eu&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetProffesionSkillTierModels.Root> GetProffesionSkillTires(int proffesionId)
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var blizzNamespace = _settingsService.BlizzApiSettings.StaticNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;

            var client = new RestClient($"https://{region}.api.blizzard.com/data/wow/profession/{proffesionId}?namespace={blizzNamespace}&locale={locale}&access_token={token}");
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);

            var proffesionSkillTires = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetProffesionSkillTierModels.Root>(resp.Content);
            return proffesionSkillTires;
        }

        // get proffesion items URL: https://us.api.blizzard.com/data/wow/profession/164/skill-tier/2477?namespace=static-us&locale=en_US&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetProffesionRecipesModels.Root> GetProffesionRecipes(int proffesionId, int proffesionTierId)
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var blizzNamespace = _settingsService.BlizzApiSettings.StaticNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;

            var client = new RestClient($"https://{region}.api.blizzard.com/data/wow/profession/{proffesionId}/skill-tier/{proffesionTierId}?namespace={blizzNamespace}&locale={locale}&access_token={token}");
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);

            var recipes = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetProffesionRecipesModels.Root>(resp.Content);
            return recipes;
        }

       
        // get itemMedia URL: https://eu.api.blizzard.com/data/wow/media/item/172357?namespace=static-eu&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetItemMediaModels.Root> GetItemMedia(int itemId)
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var blizzNamespace = _settingsService.BlizzApiSettings.StaticNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;

            var client = new RestClient($"https://{region}.api.blizzard.com/data/wow/media/item/{itemId}?namespace={blizzNamespace}&locale={locale}&access_token={token}");
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);

            var itemMedia = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetItemMediaModels.Root>(resp.Content);
            return itemMedia;
        }

        // get recipe URL: https://eu.api.blizzard.com/data/wow/recipe/42792?namespace=static-eu&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
        public async Task<Models.BlizzApiModels.GetRecipeInformationModels.Root> GetRecipeInformation(int recipeId)
        {
            if (Session.Instance.Token.IsTokenExpired())
                await _tokenService.GetToken();
            var blizzNamespace = _settingsService.BlizzApiSettings.StaticNamespace;
            var token = Session.Instance.Token.AccessToken;
            var region = _settingsService.BlizzApiSettings.Region;
            var locale = _settingsService.BlizzApiSettings.Locale;

            var client = new RestClient($"https://{region}.api.blizzard.com/data/wow/recipe/{recipeId}?namespace={blizzNamespace}&locale={locale}&access_token={token}");
            var request = new RestRequest(Method.GET);
            var resp = await client.ExecuteAsync(request);

            var recipeInfo = JsonConvert.DeserializeObject<Models.BlizzApiModels.GetRecipeInformationModels.Root>(resp.Content);
            return recipeInfo;
        }

        // get itemDetails URL: https://en.api.blizzard.com/data/wow/item/172357?namespace=static-en&locale=en_GB&access_token=USBeKxE9P5oBXjtE860Wx1Mw3TVgJjuyKk
    }
}
