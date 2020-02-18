using System;

namespace DRM_Api.Core.Entities.DTO
{
	public class UserFileResource
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string MimeType { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}