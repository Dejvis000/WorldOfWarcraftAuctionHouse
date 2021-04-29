using System.Threading.Tasks;
using WoWAuctionHouse.Models;

namespace WoWAuctionHouse.Services.FilesService
{
    public interface IFilesService
    {
        Task<string> GetProffesionImage(int proffesionId);
        Task<string> GetItemImage(int itemId);
        Task<ExpansionModel> GetExpansionImage(string expansion);

    }
}
