using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using JewerlyGala.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JewerlyGala.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            //connectionString = "Server=(localdb)\\mssqllocaldb;Database=JewerlyGala;Trusted_Connection=True;MultipleActiveResultSets=true;";
            
            
            services.AddDbContext<JewerlyDbContext>(options =>
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(JewerlyDbContext).Assembly.FullName)));

            services.AddScoped<IItemModelRepository, ItemModelsRepository>();
        }
    }
}
