using DirectoryService.Application;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Infrastructure.Postgres.Repositories;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<LocationsService>();
builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateLocationValidator>();

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
