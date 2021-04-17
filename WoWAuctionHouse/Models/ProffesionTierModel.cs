namespace WoWAuctionHouse.Models
{
    public class ProffesionTierModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public string ImageURL { get; set; }
        public int Order { get; set; }
    }
}
