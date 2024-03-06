using RoomApp.Abstractions;
using RoomApp.Models;

namespace RoomApp.Services
{
	public class RoomServiceExtensions : IEndpointDefinition
	{

		public void RegisterEndpoints(WebApplication app)
		{
			app.MapGet("/rooms", async (RoomService roomService) =>
			{
				var rooms = await roomService.GetAllRooms();
				return Results.Ok(rooms);
			});

			// Get Room by Id
			app.MapGet("/rooms/{id}", async (int id, RoomService roomService) =>
			{
				var room = await roomService.GetRoomById(id);

				if (room == null)
				{
					return Results.NotFound("Room not found");
				}

				return Results.Ok(room);
			});

			// Add Room
			app.MapPost("/rooms", async (Room room, RoomService roomService) =>
			{
				var addedRoom = await roomService.AddRoom(room);
				return Results.Created($"/rooms/{addedRoom.Id}", addedRoom);
			});

			// Update Room
			app.MapPut("/rooms/{id}", async (int id, Room room, RoomService roomService) =>
			{
				room.Id = id; // Ensure the Id matches the URL parameter

				await roomService.UpdateRoom(room);
				return Results.NoContent();
			});

			// Delete Room
			app.MapDelete("/rooms/{id}", async (int id, RoomService roomService) =>
			{
				await roomService.DeleteRoom(new Room { Id = id }); // Create a Room with the provided Id
				return Results.NoContent();
			});
		}
	}

}
