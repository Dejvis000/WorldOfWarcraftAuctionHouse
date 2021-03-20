using System.Text;
using WoWAuctionHouse.Models;

namespace WoWAuctionHouse.Extensions
{
    public static class LongExtensions
    {
        public static GoldModel ToGold(this long value)
        {
            var sb = new StringBuilder();
            var stringValue = value.ToString();
            if (!(stringValue.Length >= 6))
                for (var i = 0; i < 6 - stringValue.Length; i++)
                {
                    sb.Append("000");
                }
            stringValue = sb + stringValue;
            var bronze = int.Parse(stringValue.Substring(stringValue.Length - 2, 2));
            var silver = int.Parse(stringValue.Substring(stringValue.Length - 4, 2));
            var gold = int.Parse(stringValue.Substring(0, stringValue.Length - 4));

            return new GoldModel
            {
                Bronze = bronze,
                Gold = gold,
                Silver = silver,
                FullPrice = value
            };
        }
    }
}
