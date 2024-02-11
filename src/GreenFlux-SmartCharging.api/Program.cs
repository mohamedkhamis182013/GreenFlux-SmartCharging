using GreenFlux_SmartCharging.api.Filters;
using GreenFlux_SmartCharging.Application;
using GreenFlux_SmartCharging.Infrastructure;
using GreenFlux_SmartCharging.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

SeedDatabase(app);

app.Run();


static void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}
public partial class Program
{
}