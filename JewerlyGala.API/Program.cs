using JewerlyGala.Infrastructure.Extensions;
using JewerlyGala.Application.Extensions;
using System.Text.Json.Serialization;
using Serilog;
using JewerlyGala.API.Middlewares;
using JewerlyGala.Domain.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;


//using ZymLabs.NSwag.FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(configure => {
    //configure.SwaggerDoc("V1", new OpenApiInfo
    //{
    //    Version = "V1",
    //    Title = "WebAPI",
    //    Description = "JewerlyGala WebAPI"
    //});

    configure.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });

    configure.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ErrorHandlingMiddle>();

// add services for application layer
builder.Services.AddApplication();

// add services for infractucture layer
builder.Services.AddInfrastructure(builder.Configuration);

// configure serilog 
builder.Host.UseSerilog((context, configuration) => {
    configuration
    .ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlingMiddle>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();

//app.MapSwagger();

app.UseAuthorization();

app.UseAuthentication();


app.MapControllers();

app.Run();
