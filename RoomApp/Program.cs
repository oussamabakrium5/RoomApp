using Microsoft.EntityFrameworkCore;
using RoomApp.Data;
using RoomApp.Infrastructure;
using RoomApp.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRoomRepository, RoomRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapRoomEndpoints();

app.Run();
