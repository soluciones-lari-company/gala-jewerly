using JewerlyGala.Domain.Constans;
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
