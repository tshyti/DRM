namespace DRM_Api.Core.Entities.DTO
{
	public class TokenRefreshRequest
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}