using System.Collections.Generic;
using RoomApp.Models;

namespace RoomApp.Interfaces
{
    public interface IRoomRepository
    {
        Room GetById(int id);
        IEnumerable<Room> GetAll();
        void Add(Room room);
        void Update(Room room);
        void Delete(int id);
    }
}
