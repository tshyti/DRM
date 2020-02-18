using System;
using System.Collections.Generic;
using System.Text;

namespace DRM_Api.Core.Entities
{
	public class UserRoles : BaseEntity
	{
		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid RoleId { get; set; }
		public Role Role { get; set; }
	}
}
