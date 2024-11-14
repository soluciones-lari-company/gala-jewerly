using JewerlyGala.Infrastructure.Extensions;
using JewerlyGala.Application.Extensions;
using System.Text.Json.Serialization;
using Serilog;


//using ZymLabs.NSwag.FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// add services for infractucture layer
builder.Services.AddInfrastructure(builder.Configuration);

// add services for application layer
builder.Services.AddApplication();

// configure serilog 
builder.Host.UseSerilog((context, configuration) => {
    configuration
    .ReadFrom.Configuration(context.Configuration);
    //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
    ////.WriteTo.File("Logs/Jewerly-API.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
    //.WriteTo.Console();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<FluentValidationSchemaProcessor>(provider =>
//{
//    var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
//    var loggerFactory = provider.GetService<ILoggerFactory>();

//    return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
//});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
