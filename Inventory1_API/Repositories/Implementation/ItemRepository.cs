using Inventory1_API.Data;
using Inventory1_API.Models.Domain;
using Inventory1_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventory1_API.Repositories.Implementation
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Item> CreateAsync(Item item)
        {
            await dbContext.Items.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> DeleteAsync(Guid id)
        {
            var existingItem = await dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (existingItem != null)
            {
                dbContext.Items.Remove(existingItem);
                await dbContext.SaveChangesAsync();
                return existingItem;
            }

            return null;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await dbContext.Items.Include(x => x.Supplier).ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            return await dbContext.Items.Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Item?> GeyByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Item?> UpdateAsync(Item item)
        {
             var existingItem = await dbContext.Items.Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Id == item.Id);

            if(existingItem == null)
            {
                return null;
            }

            dbContext.Entry(existingItem).CurrentValues.SetValues(item);

            existingItem.Supplier = item.Supplier;

            await dbContext.SaveChangesAsync();

            return item;
        }
    }
}
