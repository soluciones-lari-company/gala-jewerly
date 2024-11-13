using FluentValidation;
using FluentValidation.AspNetCore;
using JewerlyGala.Application.ItemModels;
using JewerlyGala.Application.Services.ItemModels;
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
        }

        //public static IServiceCollection AddApplication(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //    services.AddScoped<IItemModelService, ItemModelService>();
        //    //services.AddMediatR(cfg => {
        //    //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //    //    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        //    //    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        //    //    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //    //    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        //    //});

        //    return services;
        //}
    }
}
