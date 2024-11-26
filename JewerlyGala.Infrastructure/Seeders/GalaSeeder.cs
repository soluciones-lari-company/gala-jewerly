using JewerlyGala.Domain.Constans;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace JewerlyGala.Infrastructure.Seeders
{
    public interface IGalaSeeder
    {
        IEnumerable<IdentityRole> GetRoles();
        Task Seed();
    }

    public class GalaSeeder(JewerlyDbContext dbContext) : IGalaSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                //var roles = GetRoles();

                //dbContext.
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();

                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Suppliers.Any())
            {
                var suppliers = GetSuppliers();

                dbContext.Suppliers.AddRange(suppliers);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.ItemMaterials.Any())
            {
                var materials = GetMaterials();

                dbContext.ItemMaterials.AddRange(materials);
                await dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = [
                    new Supplier { SupplierName = "Epoka de oro" },
                    new Supplier { SupplierName = "Thiago" },
                    new Supplier { SupplierName = "Broqueles Sanchez" },
                    new Supplier { SupplierName = "Prisma" },
                    new Supplier { SupplierName = "Farava" },
                    new Supplier { SupplierName = "Dajaos" },
                ];

            return suppliers;
        }

        public IEnumerable<ItemMaterial> GetMaterials()
        {
            List<ItemMaterial> materials = [
                    new ItemMaterial { MaterialName = "Oro 10k" },
                    new ItemMaterial { MaterialName = "Oro 14k" },
                    new ItemMaterial { MaterialName = "Plata 925" },
                ];

            return materials;
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = [
                    new (UserRoles.User) { NormalizedName =  UserRoles.User.ToUpper()},
                    new (UserRoles.Owner) { NormalizedName =  UserRoles.Owner.ToUpper()},
                    new (UserRoles.Admin) { NormalizedName =  UserRoles.Admin.ToUpper()},
                ];

            return roles;
        }
    }
}
