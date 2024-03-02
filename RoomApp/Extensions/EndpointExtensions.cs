using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using RoomApp.Infrastructure;
using RoomApp.Interfaces;
using RoomApp.Models;

public static class EndpointExtensions
{
    public static void MapRoomEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/rooms", async context =>
        {
            try
            {
                var roomRepository = context.RequestServices.GetRequiredService<IRoomRepository>();
                var rooms = roomRepository.GetAll();
                await context.Response.WriteAsJsonAsync(rooms);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        });

        endpoints.MapGet("/rooms/{id}", async context =>
        {
            try
            {
                var id = int.Parse(context.Request.RouteValues["id"].ToString());
                var roomRepository = context.RequestServices.GetRequiredService<IRoomRepository>();
                var room = roomRepository.GetById(id);
                if (room == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }
                await context.Response.WriteAsJsonAsync(room);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        });

        endpoints.MapPost("/rooms", async context =>
        {
            try
            {
                var room = await context.Request.ReadFromJsonAsync<Room>();
                if (room == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                var roomRepository = context.RequestServices.GetRequiredService<IRoomRepository>();
                roomRepository.Add(room);
                context.Response.StatusCode = StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        });

        endpoints.MapPut("/rooms/{id}", async context =>
        {
            try
            {
                var id = int.Parse(context.Request.RouteValues["id"].ToString());
                var room = await context.Request.ReadFromJsonAsync<Room>();
                if (room == null || room.Id != id)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                var roomRepository = context.RequestServices.GetRequiredService<IRoomRepository>();
                var existingRoom = roomRepository.GetById(id);
                if (existingRoom == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }
                room.Id = id;
                roomRepository.Update(room);
                context.Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        });

        endpoints.MapDelete("/rooms/{id}", async context =>
        {
            try
            {
                var id = int.Parse(context.Request.RouteValues["id"].ToString());
                var roomRepository = context.RequestServices.GetRequiredService<IRoomRepository>();
                var existingRoom = roomRepository.GetById(id);
                if (existingRoom == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                roomRepository.Delete(id);
                context.Response.StatusCode = StatusCodes.Status204NoContent;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        });
    }
}
