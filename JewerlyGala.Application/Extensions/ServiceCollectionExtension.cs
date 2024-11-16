using FluentValidation;
using FluentValidation.AspNetCore;
using JewerlyGala.Application.ItemModels;
using JewerlyGala.Application.Services.ItemModels;
using JewerlyGala.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace JewerlyGala.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
            services.AddScoped<IItemModelService, ItemModelService>();

            services.AddMediatR(c => c.RegisterServicesFromAssembly(applicationAssembly));
            services.AddAutoMapper(applicationAssembly);
            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();
        }
    }
}
