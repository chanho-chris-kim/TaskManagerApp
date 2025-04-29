using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Models;  
using TaskManagerBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // React frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Use CORS middleware
app.UseCors("AllowLocalhost");

// Other configurations...
app.UseHttpsRedirection();

// Use Swagger in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
