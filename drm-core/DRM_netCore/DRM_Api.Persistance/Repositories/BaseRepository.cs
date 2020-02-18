using DRM_Api.Core.Entities;
using DRM_Api.Core.Repositories;
using DRM_Api.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DRM_Api.Persistance.Repositories
{
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity: BaseEntity
	{
		protected readonly AppDbContext _context;

		public BaseRepository(AppDbContext context)
		{
			_context = context;
		}

		public virtual void Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}

		public virtual async Task<TEntity> GetByIdAsync(Guid id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public virtual async Task AddAsync(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
		}

		public virtual void Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
		}
	}
}
