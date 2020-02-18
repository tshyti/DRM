using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Repositories;
using DRM_Api.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DRM_Api.Persistance.Repositories
{
	public class RefreshTokenRepository : IRefreshTokenRepository
	{
		private readonly AppDbContext _context;

		public RefreshTokenRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<RefreshToken>> GetAllAsync()
		{
			return await _context.RefreshTokens.ToArrayAsync();
		}

		public async Task<RefreshToken> GetByIdAsync(Guid id)
		{
			return await _context.RefreshTokens.FindAsync(id);
		}

		public async Task AddAsync(RefreshToken entity)
		{
			await _context.RefreshTokens.AddAsync(entity);
		}

		public void Update(RefreshToken entity)
		{
			_context.RefreshTokens.Update(entity);
		}

		public void Delete(RefreshToken entity)
		{
			_context.RefreshTokens.Remove(entity);
		}
	}
}