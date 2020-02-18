using System;

namespace DRM_Api.Core.Entities.DTO
{
	public class UserResource
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public int UploadNo { get; set; }
		public int DownloadNo { get; set; }
	}
}
