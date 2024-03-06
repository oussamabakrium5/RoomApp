using Microsoft.EntityFrameworkCore;
using RoomApp.Abstractions;
using RoomApp.Data;
using RoomApp.Infrastructure;
using RoomApp.Interfaces;
using RoomApp.Models;
using RoomApp.Services;

namespace RoomApp.Extensions
{
	public static class MinimalApiExtensions
	{
		public static void RegisterServices(this WebApplicationBuilder builder)
		{
			
			var cs = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(cs));
			builder.Services.AddScoped<RoomService>();
			builder.Services.AddScoped<IRepository<Room>, Repository<Room>>();
		}

		public static void RegisterEndpointDefinitions(this WebApplication app)
		{
			var endpointDefinitions = typeof(Program).Assembly
				.GetTypes()
				.Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
					&& !t.IsAbstract && !t.IsInterface)
				.Select(Activator.CreateInstance)
				.Cast<IEndpointDefinition>();

			foreach (var endpointDef in endpointDefinitions)
			{
				endpointDef.RegisterEndpoints(app);
			}
		}
	}
}
