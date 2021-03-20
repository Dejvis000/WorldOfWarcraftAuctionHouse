using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWAuctionHouse.Models.SessionModels;

namespace WoWAuctionHouse.Common
{
    public class Session
    {
        private static SessionModel _instance = null;
        private Session() { }

        public static SessionModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionModel();
                return _instance;
            }
        }
    }
}
