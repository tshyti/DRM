using System;
using System.Collections.Generic;
using System.Text;

namespace DRM_Api.Core.Entities.DTO
{
	public class UserRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public Guid RoleID { get; set; }
	}
}
