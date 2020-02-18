using DRM_Api.Core.Entities;
using DRM_Api.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DRM_Api.Core.Services
{
	public interface IUserService
	{
		Task<IEnumerable<UserResource>> GetAllUsersAsync();
		Task<UserResource> AddUserAsync(UserRequest userData);
		Task<AuthenticatedUserModel> AuthenticateAsync(string username, string password);
		IEnumerable<RoleResource> GetUserRolesAsync(Guid userId);
		Task<AuthenticatedUserModel> ValidateRefreshToken(Guid userId, string refreshToken);
		Task LogOut(Guid userId);
		Task<UserStatsResource> GetUserStats(Guid userId);
	}
}
