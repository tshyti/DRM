using DRM_Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DRM_Api.Core.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetByUsernameAsync(string username);
		Task<User> GetByEmailAsync(string email);
		IEnumerable<Role> GetRolesForUser(Guid userId);
		Task<int> GetNumberOfActivefilesForUser(Guid userId);
	}
}
