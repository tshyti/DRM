using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRM_Api.Core.Entities.DTO;

namespace DRM_Api.Core.Services
{
	public interface IUserFileService
	{
		Task<IEnumerable<UserFileResource>> GetUserFiles(Guid userId);
		Task<UserFileResource> UploadUserFile(UserFileUploadData fileData, Guid userId);
		Task<UserFileDownloadResource> DownloadUserFile(Guid userFileId, Guid userId);
		Task<UserFileResource> DeleteUserFile(Guid userFileId, Guid userId);
	}
}