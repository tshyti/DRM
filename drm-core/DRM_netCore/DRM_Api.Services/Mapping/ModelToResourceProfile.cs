using AutoMapper;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Entities.DTO;

namespace DRM_Api.Services.Mapping
{
	public class ModelToResourceProfile : Profile
	{
		public ModelToResourceProfile()
		{
			// User
			CreateMap<User, UserResource>();
			CreateMap<User, UserRequest>();
			CreateMap<User, AuthenticatedUserModel>();
			// Role
			CreateMap<Role, RoleResource>();
			CreateMap<Role, RoleModel>();
			// UserFile
			CreateMap<UserFile, UserFileResource>()
				.ForMember(x => x.CreatedDate, 
					options => options.MapFrom(src => src.CreatedOn))
				.ForMember(x => x.ModifiedDate, 
					options => options.MapFrom(src => src.ModifiedOn));
		}
	}
}
