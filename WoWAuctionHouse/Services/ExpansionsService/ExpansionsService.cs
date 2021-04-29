using System;
using System.Collections.Generic;
using System.Linq;
using WoWAuctionHouse.Models;

namespace WoWAuctionHouse.Services.ExpansionsService
{
    public class ExpansionsService : IExpansionsService
    {
        private static Dictionary<string, ExpansionModel> Expansions = new Dictionary<string, ExpansionModel>();

        public ExpansionsService()
        {
            AddExpansions();
        }

        public ExpansionModel GetExpansionByKey(string key)
        {
            var splited = key.Split(' ');
            if (splited.Length == 1)
                return Expansions["Classic"];
            if (splited.Length > 1)
            {
                var keys = Expansions.Keys.Where(x => x.Contains(splited[0]));
                if (keys.Any())
                    return Expansions[keys.First()];
            }
            return new ExpansionModel();
        }

        private void AddExpansions()
        {
            try
            {
                Expansions.Add("Classic", new ExpansionModel
                {
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/2/28/New_WorldOfWarcraft_logo_small.png/revision/latest/scale-to-width-down/280?cb=20131220175834",
                    Name = "Classic",
                    Number = 0
                });

                Expansions.Add("Outland", new ExpansionModel
                {
                    Name = "The Burning Crusade",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/6/65/TBCLogo.png/revision/latest/scale-to-width-down/399?cb=20070804234352",
                    Number = 1
                });

                Expansions.Add("Northrend", new ExpansionModel
                {
                    Name = "Wrath of the Lich King",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/f/f0/WrathLogo.png/revision/latest/scale-to-width-down/400?cb=20070804235127",
                    Number = 2
                });

                Expansions.Add("Cataclysm", new ExpansionModel
                {
                    Name = "Cataclysm",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/4/4e/Cataclysmlogo.png/revision/latest/scale-to-width-down/428?cb=20100112200816",
                    Number = 3
                });

                Expansions.Add("Pandaria", new ExpansionModel
                {
                    Name = "Mists of Pandaria",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/8/8e/MistsofPandariaLogo.png/revision/latest/scale-to-width-down/428?cb=20111112175925",
                    Number = 4
                });

                Expansions.Add("Draenor", new ExpansionModel
                {
                    Name = "Warlords of Draenor",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/b/b7/WarlordsofDraenorLogo_Shadow.png/revision/latest/scale-to-width-down/481?cb=20131108233344",
                    Number = 5
                });

                Expansions.Add("Legion", new ExpansionModel
                {
                    Name = "Legion",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/a/ab/LegionLogo_lessglow.png/revision/latest/scale-to-width-down/400?cb=20150807024750",
                    Number = 6
                });

                Expansions.Add("Kul Tiran/Zandalari", new ExpansionModel
                {
                    Name = "Battle for Azeroth",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/6/6b/WoW_Battle_for_Azeroth_Logo.png/revision/latest/scale-to-width-down/400?cb=20171104032906",
                    Number = 7
                });

                Expansions.Add("Shadowlands", new ExpansionModel
                {
                    Name = "Shadowlands",
                    IconUrl = "https://static.wikia.nocookie.net/wowwiki/images/c/c2/Shadowlandsbutton.png/revision/latest/scale-to-width-down/400?cb=20191103121430",
                    Number = 8
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
