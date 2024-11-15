using JewerlyGala.Infrastructure.Extensions;
using JewerlyGala.Application.Extensions;
using System.Text.Json.Serialization;
using Serilog;
using JewerlyGala.API.Middlewares;
using JewerlyGala.Domain.Identity;
using JewerlyGala.API.Extensions;

//using ZymLabs.NSwag.FluentValidation;


var builder = WebApplication.CreateBuilder(args);
//var provider = TimeProvider.System;

//builder.Services.AddSingleton(provider);
builder.AddPresentation();

// add services for application layer
builder.Services.AddApplication();

// add services for infractucture layer
builder.Services.AddInfrastructure(builder.Configuration);


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

app.MapIdentityApi<User>();

app.UseAuthorization();

app.UseAuthentication();


app.MapControllers();

app.Run();
