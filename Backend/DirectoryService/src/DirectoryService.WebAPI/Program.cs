using DirectoryService.Infrastructure.Postgres;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<AppDbContext>(_ => new AppDbContext(
    builder.Configuration.GetConnectionString("DirectoryServiceDb")!
));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

if (!app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapHealthChecks("/health");
app.MapControllers();
app.Run();
