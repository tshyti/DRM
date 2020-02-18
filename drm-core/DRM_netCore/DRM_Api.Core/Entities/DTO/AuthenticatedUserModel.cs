using System;
using System.Collections;
using System.Collections.Generic;

namespace DRM_Api.Core.Entities.DTO
{
	public class AuthenticatedUserModel
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; } 
		public string UserRole { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		public DateTime? ExpirationTime { get; set; }
	}
}