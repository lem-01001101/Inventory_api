using Inventory1_API.Data;
using Inventory1_API.Models.Domain;
using Inventory1_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventory1_API.Repositories.Implementation
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SupplierRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            await dbContext.Suppliers.AddAsync(supplier);
            await dbContext.SaveChangesAsync();

            return supplier;
        }

        public async Task<Supplier?> DeleteAsync(Guid id)
        {
            var existingSupplier = await dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);

            if (existingSupplier == null)
            {
                return null;
            }

            dbContext.Suppliers.Remove(existingSupplier);
            await dbContext.SaveChangesAsync();
            return existingSupplier;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await dbContext.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetById(Guid id)
        {
            return await dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier?> UpdateAsync(Supplier supplier)
        {
            var existingSupplier = await dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == supplier.Id);

            if(existingSupplier != null)
            {
                dbContext.Entry(existingSupplier).CurrentValues.SetValues(supplier);
                await dbContext.SaveChangesAsync();
                return supplier;
            }
            
            return null;
        }
    }
}
