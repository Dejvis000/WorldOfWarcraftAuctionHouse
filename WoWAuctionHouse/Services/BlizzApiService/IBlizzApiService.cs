using System.Threading.Tasks;

namespace WoWAuctionHouse.Services.BlizzApiService
{
    public interface IBlizzApiService
    {
        Task<int> GetRealmId();
        Task<Models.BlizzApiModels.GetAuctionsModels.Root> GetAuctions();
        Task<Models.BlizzApiModels.GetProffesionModels.Root> GetProffesions();
        Task<Models.BlizzApiModels.GetProffesionMediaModels.Root> GetProffesionMedia(int proffesionId);
        Task<Models.BlizzApiModels.GetProffesionSkillTierModels.Root> GetProffesionSkillTires(int proffesionId);
        Task<Models.BlizzApiModels.GetProffesionRecipesModels.Root> GetProffesionRecipes(int proffesionId, int proffesionTierId);
        Task<Models.BlizzApiModels.GetItemMediaModels.Root> GetItemMedia(int itemId);
        Task<Models.BlizzApiModels.GetRecipeInformationModels.Root> GetRecipeInformation(int recipeId);
    }
}
