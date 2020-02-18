using System;
using System.Collections.Generic;
using System.Text;

namespace DRM_Api.Core.Entities
{
	public class User : BaseEntity
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public int UploadNo { get; set; }
		public int DownloadNo { get; set; }
		public bool IsActive { get; set; }
		public Guid? RefreshTokenId { get; set; }
		public RefreshToken RefreshToken { get; set; }
		public IList<UserRoles> UserRoles { get; set; }
		public IList<UserFile> UserFiles { get; set; }
	}
}
