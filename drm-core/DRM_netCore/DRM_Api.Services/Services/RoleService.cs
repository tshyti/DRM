using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Repositories;
using DRM_Api.Core.UnitOfWork;
using DRM_Api.Services.Helpers;

namespace DRM_Api.Services.Services
{
	public class RoleService : IRoleService
	{
		private readonly IRepository<Role> _roleRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public RoleService(IRepository<Role> roleRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_roleRepository = roleRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<RoleModel>> GetAllAsync()
		{
			try
			{
				var roles = await _roleRepository.GetAllAsync();
				// I am getting only active roles here. (Not the proper way to do this)
				roles = roles.Where(x => x.IsActive == true);
				return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(roles);
			}
			catch (Exception ex)
			{
				throw new BaseException(500, ex.Message);
			}
		}

		public Task<RoleModel> AddAsync(RoleResource roleResource)
		{
			throw new NotImplementedException();
		}

		public Task<RoleModel> UpdateAsync(Guid id, RoleResource roleResource)
		{
			throw new NotImplementedException();
		}

		public Task<RoleModel> Delete(RoleResource roleResource)
		{
			throw new NotImplementedException();
		}
	}
}
