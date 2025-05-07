using FluentValidation;
using FluentValidation.AspNetCore;
using JewerlyGala.Application.Common.Behaviours;
using JewerlyGala.Application.ItemModels;
using JewerlyGala.Application.Services.ItemModels;
using JewerlyGala.Application.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JewerlyGala.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

            services.AddAutoMapper(applicationAssembly);
            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
            services.AddMediatR(c => {
                c.RegisterServicesFromAssembly(applicationAssembly);
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            services.AddScoped<IItemModelService, ItemModelService>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();
        }
    }
}
