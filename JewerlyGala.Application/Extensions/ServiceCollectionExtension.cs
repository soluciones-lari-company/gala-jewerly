using JewerlyGala.Application.ItemModels;
using JewerlyGala.Application.Services.ItemModels;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JewerlyGala.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IItemModelService, ItemModelService>();
        }
    }
}
