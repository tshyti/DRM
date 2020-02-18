namespace DRM_Api.Core.Entities.DTO
{
	public class UserFileUploadData
	{
		public string Name { get; set; }
		public string MimeType { get; set; }
		// This comes as string64 encoded data
		public string Data { get; set; }
	}
}