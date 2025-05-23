using JewerlyGala.API.Middlewares;
using JewerlyGala.API.Services;
using JewerlyGala.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using ZymLabs.NSwag.FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace JewerlyGala.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {

            //builder.Services.AddSwaggerGen(static c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "Joyeria Gala Servicios API",
            //        Description = "API for manegement Gala Joyeria",
            //        TermsOfService = new Uri("https://example.com/terms"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Example Contact",
            //            Url = new Uri("https://example.com/contact")
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Example License",
            //            Url = new Uri("https://example.com/license")
            //        }
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //    {
            //        {
            //            new OpenApiSecurityScheme {
            //                Reference = new OpenApiReference {
            //                    Id = "bearerAuth",
            //                        Type = ReferenceType.SecurityScheme
            //                }
            //            },
            //            new List < string > ()
            //        }
            //    });
            //});

            // Customise default API behaviour
            builder.Services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddOpenApiDocument((configure, serviceProvider) =>
            {
                var fluentValidationSchemaProcessor = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();
                //// Add the fluent validations schema processor
                configure.SchemaSettings.SchemaProcessors.Add(fluentValidationSchemaProcessor);

                configure.Title = "Joyeria Gala API";
                configure.Version = "v1";
                configure.Description = "API for manegement Gala Joyeria";

                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme()
                {
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}.",
                });
                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "Policy1",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:5173","https://gala-joyeria.com")
                                      .AllowAnyHeader()
                                              .AllowAnyMethod().AllowCredentials();

                                      //policy.AllowAnyOrigin()
                                      // .AllowAnyMethod()
                                      // .AllowAnyHeader();
                                  });

            });

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddScoped<FluentValidationSchemaProcessor>(provider =>
            {
                var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
                var loggerFactory = provider.GetService<ILoggerFactory>();

                return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
            });

            builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options =>
            {
                options.BearerTokenExpiration = TimeSpan.FromDays(12);
            });

            //builder.Services.AddScoped<ErrorHandlingMiddle>();

            // configure serilog 
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                .ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
