using PackageTracking.API.Extensions;
using PackageTracking.API.Middlewares;
using PackageTracking.Application.Extensions;
using PackageTracking.Domain.Entities;
using PackageTracking.Infrastructure.Extensions;
using PackageTracking.Infrastructure.Seeders.Interfaces;
using Serilog;
try
{
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
    app.UseMiddleware<RequestTimeLoggingMiddleware>();

    app.UseSerilogRequestLogging();

    //check if is in development
    if(app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
