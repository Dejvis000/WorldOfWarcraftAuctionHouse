using RestSharp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> GetExpansionImage(string expansion)
        {
            var directoryPath = Path.Combine(RootPath, expansion);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            var fileName = expansion + ".png";
            var filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
                return filePath;
            var expansionObject = _expansionsService.GetExpansionByKey(expansion);

            Task.Factory.StartNew(() => SaveFile(itemMediaUrl, filePath));

            return expansionObject.IconUrl;
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
            var itemMediaUrl = itemMedia.assets.First().value;

            Task.Factory.StartNew(() => SaveFile(itemMediaUrl, filePath));

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
