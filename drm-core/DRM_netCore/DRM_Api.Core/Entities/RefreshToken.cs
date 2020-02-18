using System;

namespace DRM_Api.Core.Entities
{
	public class RefreshToken
	{
		public Guid Id { get; set; }
		public string Token { get; set; }
		public DateTime? Expiration { get; set; }
	}
}