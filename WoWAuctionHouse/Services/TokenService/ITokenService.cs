using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WoWAuctionHouse.Models.SessionModels;

namespace WoWAuctionHouse.Services.TokenService
{
    public interface ITokenService
    {
        Task<TokenModel> GetToken();
    }
}
