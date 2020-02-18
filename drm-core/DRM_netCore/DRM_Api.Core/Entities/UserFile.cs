using System;

namespace DRM_Api.Core.Entities
{
	public class UserFile : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string URL { get; set; }
		public string AzureName { get; set; }
		public string MimeType { get; set; }
		public bool IsActive { get; set; }
		public Guid OwnerId { get; set; }
		public User Owner { get; set; }
	}
}