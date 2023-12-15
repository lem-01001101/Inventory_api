namespace Inventory1_API.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Category {  get; set; }
        public string Note { get; set; }
        public DateTime LastInventory { get; set; }
        public string LastManager { get; set; }

        public ICollection<Supplier> Supplier { get; set; }
    }
}
