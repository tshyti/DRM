using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Repositories;
using DRM_Api.Core.Services;
using DRM_Api.Core.UnitOfWork;
using DRM_Api.Services.Helpers;
using Microsoft.Extensions.Options;

namespace DRM_Api.Services.Services
{
	public class UserFileService : IUserFileService
	{
		private readonly IUserFileRepository _userFileRepository;
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IOptions<BlobConfiguration> _blobConfiguarations;

		public UserFileService(IUserFileRepository userFileRepository, IUnitOfWork unitOfWork, 
			IMapper mapper, IOptions<BlobConfiguration> blobConfiguarations, IUserRepository userRepository)
		{
			_userFileRepository = userFileRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_blobConfiguarations = blobConfiguarations;
			_userRepository = userRepository;
		}

		public async Task<IEnumerable<UserFileResource>> GetUserFiles(Guid userId)
		{
			try
			{
				var userFiles = await _userFileRepository.GetActiveFilesForUser(userId);
				return _mapper.Map<IEnumerable<UserFile>, IEnumerable<UserFileResource>>(userFiles);
			}
			catch (Exception ex)
			{
				throw new BaseException(500, ex.Message);
			}
		}

		public async Task<UserFileResource> UploadUserFile(UserFileUploadData fileData, Guid userId)
		{
			UserFile file = new UserFile();
			file.Name = fileData.Name;
			file.IsActive = true;
			try
			{
				file.AzureName = Helper.GenerateFileName(fileData.Name);
				byte[] decodedData = Convert.FromBase64String(fileData.Data);
				file.URL = await Helper.UploadFileToBlobAsync(file.AzureName, decodedData, fileData.MimeType,
						_blobConfiguarations.Value.AccessKey, _blobConfiguarations.Value.ContainerName);
			}
			catch (Exception ex)
			{
				throw new BaseException(500, "Could not upload file.", ex.Message);
			}
			file.MimeType = fileData.MimeType;
			User user = await _userRepository.GetByIdAsync(userId);
			file.Owner = user;
			user.UploadNo++;
			file.CreatedOn = DateTime.Now;
			file.ModifiedOn = DateTime.Now;
			await _userFileRepository.AddAsync(file);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserFile, UserFileResource>(file);
		}

		public async Task<UserFileDownloadResource> DownloadUserFile(Guid userFileId, Guid userId)
		{
			UserFile file = await _userFileRepository.GetByIdAsync(userFileId);
			if (file == null)
			{
				throw new BaseException(404, "The resource does not exist.");
			}

			if (file.OwnerId != userId)
			{
				throw new BaseException(401, "You are not authorized to access this file.");
			}

			UserFileDownloadResource downloadedFile = new UserFileDownloadResource();

			downloadedFile.Name = file.Name;
			downloadedFile.MimeType = file.MimeType;
			downloadedFile.Data = await Helper.DownloadBlobFiles(file.AzureName, _blobConfiguarations.Value.AccessKey,
				_blobConfiguarations.Value.ContainerName);

			if (downloadedFile.Data != null && downloadedFile.Data.Length > 0)
			{
				file.Owner.DownloadNo++;
				await _unitOfWork.SaveChangesAsync();
				return downloadedFile;
			}
			else
			{
				throw new BaseException(500, "Could not load file data.");
			}
		}

		public async Task<UserFileResource> DeleteUserFile(Guid userFileId, Guid userId)
		{
			UserFile file = await _userFileRepository.GetByIdAsync(userFileId);
			if (file.OwnerId != userId)
			{
				throw new BaseException(401, "You are not authorized to delete this file");
			}

			file.IsActive = false;
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserFile, UserFileResource>(file);
		}
	}
}