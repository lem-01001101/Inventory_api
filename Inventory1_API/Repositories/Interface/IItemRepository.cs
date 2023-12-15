using Inventory1_API.Models.Domain;

namespace Inventory1_API.Repositories.Interface
{
    public interface IItemRepository
    {
        Task<Item> CreateAsync(Item item);

        Task<IEnumerable<Item>> GetAllAsync();

        Task<Item?> GetByIdAsync(Guid id);

        Task<Item?> UpdateAsync(Item item);

        Task<Item?> DeleteAsync(Guid id);
    }
}
