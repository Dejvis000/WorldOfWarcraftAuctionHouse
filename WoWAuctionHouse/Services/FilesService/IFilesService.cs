using System.Threading.Tasks;

namespace WoWAuctionHouse.Services.FilesService
{
    public interface IFilesService
    {
        Task<string> GetProffesionImage(int proffesionId);
        Task<string> GetItemImage(int itemId);
        Task<string> GetExpansionImage(string expansion);

    }
}
