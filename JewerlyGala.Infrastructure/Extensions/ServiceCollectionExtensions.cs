using JewerlyGala.Application.Interfaces;
using JewerlyGala.Domain.Identity;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Infrastructure.Authorization;
using JewerlyGala.Infrastructure.Persistence;
using JewerlyGala.Infrastructure.Persistence.Intereptors;
using JewerlyGala.Infrastructure.Repositories;
using JewerlyGala.Infrastructure.Repositories.Accounting;
using JewerlyGala.Infrastructure.Repositories.Sales;
using JewerlyGala.Infrastructure.Seeders;
using JewerlyGala.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;

namespace JewerlyGala.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // 
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            //connectionString = "Server=(localdb)\\mssqllocaldb;Database=JewerlyGala;Trusted_Connection=True;MultipleActiveResultSets=true;";
            
            
            services.AddDbContext<JewerlyDbContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(JewerlyDbContext).Assembly.FullName))
                .EnableSensitiveDataLogging() // allows to show details for parameters
            );

            // setup identity roles s
            services.AddIdentityApiEndpoints<User>(options =>
                options.SignIn.RequireConfirmedAccount = true
                ).AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<JewerlyGalaUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<JewerlyDbContext>();

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            // add repositories
            services.AddScoped<ISearchEngineRepository, SearchEngineRepository>();
            services.AddScoped<IItemSerieRepository, ItemSerieRepository>();
            services.AddScoped<IItemMaterialRepository, ItemMaterialRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IItemFeatureValueRepository, ItemFeatureValueRepository>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();

            services.AddScoped<IItemModelRepository, ItemModelsRepository>();

            services.AddScoped<IAccountRepository, AccountRepository>();

            // add extra services
            services.AddScoped<IDateTime, DateTimeService>();

            // add seeders data
            services.AddScoped<IGalaSeeder, GalaSeeder>();


            services.AddAuthorizationBuilder()
                .AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));
        }
    }
}
