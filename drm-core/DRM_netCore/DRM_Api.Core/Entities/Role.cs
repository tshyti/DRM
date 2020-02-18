using System.Collections.Generic;

namespace DRM_Api.Core.Entities
{
	public class Role : BaseEntity
	{
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public IList<UserRoles> UserRoles { get; set; }
	}
}
