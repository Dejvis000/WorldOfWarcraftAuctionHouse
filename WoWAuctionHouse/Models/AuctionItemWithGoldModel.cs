using WoWAuctionHouse.Extensions;

namespace WoWAuctionHouse.Models
{
    public class AuctionItemWithGoldModel
    {
        private long unitPrice;
        private long? buyOutPrice;
        public long? BuyOutPrice
        {
            get => buyOutPrice;
            set
            {
                buyOutPrice = value;
                BuyOutPriceGold = buyOutPrice.ToGold();
            }
        }
        public long UnitPrice
        {
            get => unitPrice;
            set
            {
                unitPrice = value;
                UnitPriceGold = unitPrice.ToGold();
            }
        }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public GoldModel BuyOutPriceGold { get; set; }
        public GoldModel UnitPriceGold { get; set; }
    }
}
