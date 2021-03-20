namespace WoWAuctionHouse.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemImage { get; set; }
        public int Quantity { get; set; }
        public GoldModel Gold { get; set; }
    }
}
