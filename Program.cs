using Buscador.Aplications;
using Buscador.Core.Interfaces;
using Buscador.Core.Settings;
using Buscador.Infrastructure.Data;
using Buscador.Interfaces;
using Buscador.Repositories;
using Buscador.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Usa UseSqlServer para conectar ao SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.Configure<SecuritySettings>(
    builder.Configuration.GetSection("SecuritySettings"));
builder.Services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBuscadorAplication, BuscadorAplication>();
builder.Services.AddScoped<IBuscadorRepository, BuscadorRepository>();
builder.Services.AddScoped<IUserAplication, UserAplication>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();
builder.Services.AddSingleton<IHashService, HashService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<Buscador.Middlewares.ExceptionMiddleware>();
app.MapControllers();

app.Run();
