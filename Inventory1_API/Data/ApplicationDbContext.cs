using Inventory1_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory1_API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { 
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
