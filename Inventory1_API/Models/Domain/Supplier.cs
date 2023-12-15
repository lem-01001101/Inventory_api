namespace Inventory1_API.Models.Domain
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<Item> Item { get; set;}
    }
}
