using RoomApp.Models;
using RoomApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using RoomApp.Data;

namespace RoomApp.Infrastructure
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public RoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Room GetById(int id)
        {
            return _dbContext.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Room> GetAll()
        {
            return _dbContext.Rooms.ToList();
        }

        public void Add(Room room)
        {
            _dbContext.Rooms.Add(room);
            _dbContext.SaveChanges();
        }

        public void Update(Room room)
        {
            _dbContext.Rooms.Update(room);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var roomToRemove = GetById(id);
            if (roomToRemove != null)
            {
                _dbContext.Rooms.Remove(roomToRemove);
                _dbContext.SaveChanges();
            }
        }
    }
}

