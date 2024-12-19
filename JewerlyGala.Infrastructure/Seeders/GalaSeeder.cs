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

            if (!dbContext.Accounts.Any())
            {
                var accounts = GetAccounts();

                dbContext.Accounts.AddRange(accounts);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.ItemMaterials.Any())
            {
                var materials = GetMaterials();

                dbContext.ItemMaterials.AddRange(materials);
                await dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Account> GetAccounts()
        {
            List<Account> suppliers = [
                    new Account { Name = "Gala" , IsActive = true, Comments = "Cuenta para mostrador(solo efectivo)", PaymentMethodAcceptable = "01" },
                    new Account { Name = "Gabriela Transferencias" , IsActive = true, Comments = "Cuenta para transferencias a gaby", PaymentMethodAcceptable = "03" },
                    new Account { Name = "Luis Transferencias" , IsActive = true, Comments = "Cuenta para transferencias a luis", PaymentMethodAcceptable = "03" },
                ];

            return suppliers;
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
