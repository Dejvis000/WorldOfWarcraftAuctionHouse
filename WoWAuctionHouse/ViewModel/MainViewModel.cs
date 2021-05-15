using GalaSoft.MvvmLight;
using Squirrel;
using System.Threading.Tasks;

namespace WoWAuctionHouse.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            WindowTitle = $"Auction House {version}";

            Task.Run(async () => await Update());
        }

        private string _windowTitle;

        public string WindowTitle
        {
            get => _windowTitle;
            set
            {
                Set(() => WindowTitle, ref _windowTitle, value);
            }
        }

        public async Task Update()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/Dejvis000/WorldOfWarcraftAuctionHouse", prerelease:true))
            {
                await mgr.Result.UpdateApp();
            }
        }

    }
}