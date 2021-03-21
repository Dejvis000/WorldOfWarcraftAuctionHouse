using Autofac;
using Autofac.Extras.CommonServiceLocator;
using AutoMapper;
using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using WoWAuctionHouse.Mapping;
using WoWAuctionHouse.Models.SettingsModels;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.Services.BlizzApiService;
using WoWAuctionHouse.Services.SettingsService;
using WoWAuctionHouse.Services.TokenService;
using WoWAuctionHouse.ViewModel;

namespace WoWAuctionHouse.Infrastructure
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var appConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                 .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(appConfig).CreateLogger();

            var blizzApiSettings = new BlizzApiSettingsModel();
            appConfig.GetSection("BlizzApiSettings").Bind(blizzApiSettings);

            var container = new ContainerBuilder();

            container.RegisterInstance(new SettingsService(blizzApiSettings)).As<ISettingsService>();

            container.RegisterInstance(new MainViewModel());

            container.RegisterType<AuctionsWindowModel>();

            container.RegisterType<ProffesionViewModel>();
            container.RegisterType<ProffesionSkillTierViewModel>();
            container.RegisterType<ProffesionRecipesViewModel>();

            container.RegisterType<TokenService>().As<ITokenService>();
            container.RegisterType<BlizzApiService>().As<IBlizzApiService>();
            container.RegisterType<AuctionService>().As<IAuctionService>().SingleInstance();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            container.RegisterInstance(mapConfig.CreateMapper()).As<IMapper>();

            SetupNavigation(container);
            var builder = container.Build();
            var locator = new AutofacServiceLocator(builder);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AuctionsWindowModel AuctionsWindowModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuctionsWindowModel>();
            }
        }

        public ProffesionViewModel ProffesionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProffesionViewModel>();
            }
        }

        public ProffesionSkillTierViewModel ProffesionSkillTierViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProffesionSkillTierViewModel>();
            }
        }

        public ProffesionRecipesViewModel ProffesionRecipesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProffesionRecipesViewModel>();
            }
        }

        private static void SetupNavigation(ContainerBuilder container)
        {
            var navigationService = new FrameNavigationService();

            navigationService.Configure("ProffesionView", new Uri("ProffesionView.xaml", UriKind.Relative));
            navigationService.Configure("ProffesionSkillTierView", new Uri("ProffesionSkillTierView.xaml", UriKind.Relative));
            navigationService.Configure("ProffesionRecipesView", new Uri("ProffesionRecipesView.xaml", UriKind.Relative));

            navigationService.Configure("AuctionsWindow", new Uri("AuctionsWindow.xaml", UriKind.Relative));

            container.RegisterInstance<IFrameNavigationService>(navigationService);
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}