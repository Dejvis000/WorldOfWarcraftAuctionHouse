using AutoMapper;
using WoWAuctionHouse.Models;

namespace WoWAuctionHouse.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.BlizzApiModels.GetProffesionRecipesModels.Root, ProffesionRecipesModel>().ReverseMap();
        }
    }
}
