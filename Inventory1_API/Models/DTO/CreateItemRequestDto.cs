namespace Inventory1_API.Models.DTO
{
    public class CreateItemRequestDto
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
        public DateTime LastInventory { get; set; }
        public string LastManager { get; set; }

        public Guid[] Supplier { get; set; }
    }
}
