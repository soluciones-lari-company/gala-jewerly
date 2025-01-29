using JewerlyGala.API.Middlewares;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using NSwag.Generation.Processors.Security;
using Serilog;
using System.Text.Json.Serialization;
using ZymLabs.NSwag.FluentValidation;

namespace JewerlyGala.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {

            builder.Services.AddSwaggerGen(static c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Joyeria Gala Servicios API",
                    Description = "API for manegement Gala Joyeria",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "bearerAuth",
                                    Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List < string > ()
                    }
                });
            });

            builder.Services.AddOpenApiDocument(static (configure, serviceProvider) =>
            {
                var fluentValidationSchemaProcessor = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();
                //// Add the fluent validations schema processor
                //configure.SchemaProcessors.Add(fluentValidationSchemaProcessor);

                configure.Title = "Joyeria Gala API";
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
                                      policy.WithOrigins("http://localhost:5173",
                                                          "http://www.contoso.com")
                                      .AllowAnyHeader()
                                              .AllowAnyMethod();
                                  });

            });

            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

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
                options.BearerTokenExpiration = TimeSpan.FromMinutes(10);
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
