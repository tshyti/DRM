using AutoMapper;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRM_Api.Services.Mapping
{
	public class ResourceToModelProfile : Profile
	{
		public ResourceToModelProfile()
		{
			// User
			CreateMap<UserResource, User>();
			CreateMap<UserRequest, User>();
			CreateMap<AuthenticatedUserModel, User>();
			// Role
			CreateMap<RoleResource, Role>();
		}
	}
}
