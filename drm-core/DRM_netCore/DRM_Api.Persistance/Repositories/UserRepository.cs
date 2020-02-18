using DRM_Api.Core.Entities;
using DRM_Api.Core.Repositories;
using DRM_Api.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRM_Api.Persistance.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(AppDbContext context) : base(context)
		{

		}

		public async Task<User> GetByEmailAsync(string email)
		{
			return await _context.Users.Where(x => x.Email == email && x.IsActive == true).FirstOrDefaultAsync();
		}

		public IEnumerable<Role> GetRolesForUser(Guid userId)
		{
			return _context.UserRoles
				.Where(x => x.UserId == userId)
				.Select(x => x.Role).ToList();
		}

		public async Task<int> GetNumberOfActivefilesForUser(Guid userId)
		{
			return await _context.UserFiles.CountAsync(x => x.OwnerId == userId && x.IsActive == true);
		}

		public async Task<User> GetByUsernameAsync(string username)
		{
			return await _context.Users
				.Where(x => x.Username == username && x.IsActive == true)
				.Include(x => x.UserRoles)
				.ThenInclude(x => x.Role)
				.Include(x => x.RefreshToken)
				.FirstOrDefaultAsync();
		}

		public override async Task<User> GetByIdAsync(Guid id)
		{
			return await _context.Users.Where(x => x.Id == id)
				.Include(x => x.RefreshToken)
				.FirstOrDefaultAsync();

		}
	}
}
