using JewerlyGala.Infrastructure.Extensions;
using JewerlyGala.Application.Extensions;
using System.Text.Json.Serialization;
//using ZymLabs.NSwag.FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
