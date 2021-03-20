using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWAuctionHouse.Infrastructure
{
    public interface IFrameNavigationService : INavigationService
    {
        Guid Guid { get; set; }
        object Parameter { get; }
    }
}
