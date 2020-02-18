namespace DRM_Api.Core.Entities.DTO
{
	public class UserFileDownloadResource
	{
		public string Name { get; set; }
		public string MimeType { get; set; }
		public byte[] Data { get; set; }
	}
}