using DRM_Api.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DRM_Api.Core.Services
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleModel>> GetAllAsync();
		Task<RoleModel> AddAsync(RoleResource roleResource);
		Task<RoleModel> UpdateAsync(Guid id, RoleResource roleResource);
		Task<RoleModel> Delete(RoleResource roleResource);
	}
}
