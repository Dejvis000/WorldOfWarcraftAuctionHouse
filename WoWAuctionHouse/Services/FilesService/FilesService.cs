using RestSharp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.BlizzApiService;
using WoWAuctionHouse.Services.ExpansionsService;

namespace WoWAuctionHouse.Services.FilesService
{
    public class FilesService : IFilesService
    {
        private readonly IBlizzApiService _blizzApiService;
        private readonly IExpansionsService _expansionsService;

        private readonly string RootPath = Directory.GetCurrentDirectory();
        private readonly string ItemFolder = "Items";
        private readonly string ProffesionFolder = "Proffesions";
        private readonly string ExpansionFolder = "Expansions";

        public FilesService(IBlizzApiService blizzApiService, IExpansionsService expansionsService)
        {
            _blizzApiService = blizzApiService;
            _expansionsService = expansionsService;
        }

        public async Task<ExpansionModel> GetExpansionImage(string expansion)
        {
            expansion = expansion.Split('/')[0];
            var expansionObject = _expansionsService.GetExpansionByKey(expansion);
            var directoryPath = Path.Combine(RootPath, ExpansionFolder);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            var fileName = expansionObject.Name + ".png";
            var filePath = Path.Combine(directoryPath, fileName);
            
            if (File.Exists(filePath))
            {
                expansionObject.IconUrl = filePath;
                return expansionObject;
            }

            Task.Factory.StartNew(() => SaveFile(expansionObject.IconUrl, filePath));

            return expansionObject;
        }

        public async Task<string> GetItemImage(int itemId)
        {
            var directoryPath = Path.Combine(RootPath, ItemFolder);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            var fileName = itemId + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
                return filePath;
            var itemMedia = await _blizzApiService.GetItemMedia(itemId);
            string itemMediaUrl = "";
            if (itemMedia != null)
            {
                itemMediaUrl = itemMedia.assets.First().value;
                Task.Factory.StartNew(() => SaveFile(itemMediaUrl, filePath));
            }

            return itemMediaUrl;

        }

        public async Task<string> GetProffesionImage(int proffesionId)
        {
            var directoryPath = Path.Combine(RootPath, ProffesionFolder);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            var fileName = proffesionId + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
                return filePath;
            var proffesionMedia = await _blizzApiService.GetProffesionMedia(proffesionId);
            var proffesionMediaUrl = proffesionMedia.assets.First().value;

            Task.Factory.StartNew(() => SaveFile(proffesionMediaUrl, filePath));

            return proffesionMediaUrl;
        }

        private void SaveFile(string url, string filePath)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            File.WriteAllBytes(filePath, response.RawBytes);
        }
    }
}
