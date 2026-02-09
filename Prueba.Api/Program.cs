using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prueba.Application.Interfaces;
using Prueba.Application.Managers;
using Prueba.Application.Mappers;
using Prueba.Infrastructure.Persistence;
using Prueba.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(cfg => { },typeof(ClienteProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(ProductoProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(OrdenProfile).Assembly);


// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Managers
builder.Services.AddScoped<IClienteManager, ClienteManager>();
builder.Services.AddScoped<IProductoManager, ProductoManager>();
builder.Services.AddScoped<IOrdenManager, OrdenManager>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
var corsPolicyName = "AllowBlazorClient";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", " API V1");
    });
}
app.UseCors(corsPolicyName);
app.MapControllers();

app.Run();
