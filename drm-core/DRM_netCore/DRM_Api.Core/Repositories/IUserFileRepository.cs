using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRM_Api.Core.Entities;

namespace DRM_Api.Core.Repositories
{
	public interface IUserFileRepository : IRepository<UserFile>
	{
		Task<IEnumerable<UserFile>> GetActiveFilesForUser(Guid ownerId);
	}
}