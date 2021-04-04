using Microsoft.Extensions.Logging;
using RestSharp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WoWAuctionHouse.Services.BlizzApiService;

namespace WoWAuctionHouse.Services.FilesService
{
    public class FilesService : IFilesService
    {
        private readonly IBlizzApiService _blizzApiService;

        private readonly string RootPath = Directory.GetCurrentDirectory();
        private readonly string ItemFolder = "Items";
        private readonly string ProffesionFolder = "Proffesions";

        public FilesService(IBlizzApiService blizzApiService)
        {
            _blizzApiService = blizzApiService;
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
