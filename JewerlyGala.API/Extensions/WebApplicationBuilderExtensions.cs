using JewerlyGala.API.Middlewares;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

namespace JewerlyGala.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {

            builder.Services.AddAuthentication();

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerGen(configure =>
            {
                configure.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Scheme = "bearerAuth",
                    //BearerFormat = "JWT",
                    //In = ParameterLocation.Header,
                    //Name = "Authorization",
                    //Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });

                configure.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ErrorHandlingMiddle>();

            // configure serilog 
            builder.Host.UseSerilog((context, configuration) => {
                configuration
                .ReadFrom.Configuration(context.Configuration);
            });

        }
    }
}
