namespace DRM_Api.Helpers
{
	public class TokenAppSettings
	{
		public string Key { get; set; }
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public string AccessTokenExpiration { get; set; }
		public string RefreshTokenExpiration { get; set; }
	}
}