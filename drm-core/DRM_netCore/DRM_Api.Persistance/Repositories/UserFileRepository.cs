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
	public class UserFileRepository : BaseRepository<UserFile>, IUserFileRepository
	{
		public UserFileRepository(AppDbContext context) : base(context)
		{

		}

		public override async Task<UserFile> GetByIdAsync(Guid id)
		{
			return await _context.UserFiles.Where(x => x.Id == id)
				.Include(x => x.Owner)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<UserFile>> GetActiveFilesForUser(Guid ownerId)
		{
			return await _context.UserFiles.Where(x => x.IsActive == true && x.OwnerId == ownerId).ToListAsync();
		}
	}
}