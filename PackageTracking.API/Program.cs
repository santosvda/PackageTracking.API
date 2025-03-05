using Microsoft.OpenApi.Models;
using PackageTracking.API.Extensions;
using PackageTracking.API.Middlewares;
using PackageTracking.Application.Extensions;
using PackageTracking.Domain.Entities;
using PackageTracking.Infrastructure.Extensions;
using PackageTracking.Infrastructure.Seeders.Interfaces;
using Serilog;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IReceiverSeeder>();

await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
