using AutoMapper;
using WoWAuctionHouse.Extensions;
using WoWAuctionHouse.Models;
using WoWAuctionHouse.Services.AuctionService;
using WoWAuctionHouse.ViewModel;

namespace WoWAuctionHouse.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.BlizzApiModels.GetProffesionRecipesModels.Root, ProffesionRecipesModel>().ReverseMap();

            CreateMap<AuctionItemWithGoldModel, AuctionItemModel>().ReverseMap();
        }
    }
}
