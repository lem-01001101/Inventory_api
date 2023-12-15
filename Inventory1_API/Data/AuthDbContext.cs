using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory1_API.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "23a0b3ff-5564-44f7-83e7-fdc57696bb8a";
            var writerRoleId = "01393105-9623-423a-aab0-fbba3083bbe1";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // see roles
            builder.Entity<IdentityRole>().HasData(roles);

            // create admin
            var adminUserId = "219dcdbe-5724-49d7-a767-f86a835c5788";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@warehouse.com",
                Email = "admin@warehouse.com",
                NormalizedEmail = "admin@warehouse.com".ToUpper(),
                NormalizedUserName = "admin@warehouse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Warehouse@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //give roles
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
