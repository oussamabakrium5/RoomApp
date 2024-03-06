using Microsoft.EntityFrameworkCore;
using RoomApp.Data;
using RoomApp.Interfaces;

namespace RoomApp.Infrastructure
{

	
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbSet<T> _dbSet;
		private readonly AppDbContext _appDbContext;
		public Repository(AppDbContext appDbContext)
		{
			_dbSet = appDbContext.Set<T>();
			_appDbContext = appDbContext;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Attach(entity);
			_appDbContext.Entry(entity).State = EntityState.Modified;
			await _appDbContext.SaveChangesAsync();
		}
	}

}
