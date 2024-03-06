using Microsoft.EntityFrameworkCore;
using RoomApp.Models;

namespace RoomApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
