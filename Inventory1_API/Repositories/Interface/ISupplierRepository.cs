using Inventory1_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory1_API.Repositories.Interface
{
    public interface ISupplierRepository
    {
        Task<Supplier> CreateAsync(Supplier supplier);

        Task<IEnumerable<Supplier>> GetAllAsync();

        Task<Supplier?> GetById(Guid id);

        Task<Supplier?> UpdateAsync(Supplier supplier);

        Task<Supplier?> DeleteAsync(Guid id);
    }
}
