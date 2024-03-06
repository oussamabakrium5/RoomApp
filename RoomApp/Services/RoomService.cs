using RoomApp.Interfaces;
using RoomApp.Models;

namespace RoomApp.Services
{
	public class RoomService
	{
		private readonly IRepository<Room> _roomRepository;

		public RoomService(IRepository<Room> roomRepository)
		{
			_roomRepository = roomRepository;
		}

		public async Task<IEnumerable<Room>> GetAllRooms()
		{
			return await _roomRepository.GetAllAsync();
		}

		public async Task<Room> GetRoomById(int id)
		{
			return await _roomRepository.GetByIdAsync(id);
		}

		public async Task<Room> AddRoom(Room room)
		{
			var roomModel = new Room()
			{
				Number = room.Number,
				Capacity = room.Capacity,
			};
			return await _roomRepository.AddAsync(roomModel);
		}

		public async Task UpdateRoom(Room room)
		{
			await _roomRepository.UpdateAsync(room);
		}

		public async Task DeleteRoom(Room room)
		{
			await _roomRepository.DeleteAsync(room);
		}
	}
}
