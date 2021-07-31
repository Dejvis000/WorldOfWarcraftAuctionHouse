using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using AutoUpdaterDotNET;


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
            AutoUpdater.Start("https://github.com/Dejvis000/WorldOfWarcraftAuctionHouse/releases/download/1.0.0.0/info.xml");
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

    }
}