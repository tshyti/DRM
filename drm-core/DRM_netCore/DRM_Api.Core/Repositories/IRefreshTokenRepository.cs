using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRM_Api.Core.Entities;

namespace DRM_Api.Core.Repositories
{
	public interface IRefreshTokenRepository
	{
		Task<IEnumerable<RefreshToken>> GetAllAsync();
		Task<RefreshToken> GetByIdAsync(Guid id);
		Task AddAsync(RefreshToken entity);
		void Update(RefreshToken entity);
		void Delete(RefreshToken entity);
	}
}