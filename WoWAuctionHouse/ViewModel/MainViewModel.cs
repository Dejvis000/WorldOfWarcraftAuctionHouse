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
            AutoUpdater.Start("http://rbsoft.org/updates/AutoUpdaterTest.xml");
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