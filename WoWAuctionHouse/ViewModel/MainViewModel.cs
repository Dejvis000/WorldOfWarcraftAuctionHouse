using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using AutoUpdaterDotNET;
using Octokit;
using System.Collections.Generic;
using System;

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
            IReadOnlyList<Release> releases;

            var client = new GitHubClient(new ProductHeaderValue("WorldOfWarcraftAuctionHouse"));
            releases = client.Repository.Release.GetAll("Dejvis000", "WorldOfWarcraftAuctionHouse").Result;

            AutoUpdater.Start($"https://github.com/Dejvis000/WorldOfWarcraftAuctionHouse/releases/download/{releases[0].TagName}/info.xml");
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