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

// Configuración temporal - Sin base de datos por ahora
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<ILeadService, LeadService>();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllModules", policy =>
    {
        policy.AllowAnyOrigin()
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

app.UseCors("AllowAllModules");
app.UseAuthorization();
app.MapControllers();

// COMENTAMOS TEMPORALMENTE LA INICIALIZACIÓN DE LA BASE DE DATOS
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
    await SeedData.InitializeAsync(context);
}
*/

app.Run();