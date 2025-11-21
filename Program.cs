using Marketing_LP.Core.Interfaces;
using Marketing_LP.Core.Services;
using Marketing_LP.Infrastructure.Data;
using Marketing_LP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Dependency Injection - Core Services
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();

// Dependency Injection - Repositories
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();

// CORS Configuration - PARA TODOS LOS MÓDULOS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllModules", policy =>
    {
        policy.AllowAnyOrigin()  // Para desarrollo - en producción especifica los dominios
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS - IMPORTANTE: Debe ir antes de UseAuthorization
app.UseCors("AllowAllModules");

app.UseAuthorization();
app.MapControllers();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();

    // Seed inicial de datos
    await SeedData.InitializeAsync(context);
}

app.Run();