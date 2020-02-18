using AutoMapper;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Repositories;
using DRM_Api.Core.Services;
using DRM_Api.Core.UnitOfWork;
using DRM_Api.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DRM_Api.Services.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<AuthenticatedUserModel> AuthenticateAsync(string username, string password)
		{
			var user = await _userRepository.GetByUsernameAsync(username);
			if (user == null)
			{
				throw new BaseException(400, "There is no user registered with this username.");
			}

			if (!Helper.VerifyHash(user.Password, password))
			{
				throw new BaseException(400, "Incorrect password.");
			}

			if (user.RefreshToken == null)
			{
				RefreshToken refreshToken = new RefreshToken();
				user.RefreshToken = refreshToken;
			}

			user = await AssingRefreshTokenToUser(user);

			AuthenticatedUserModel authenticatedUser = _mapper.Map<User, AuthenticatedUserModel>(user);
			authenticatedUser.RefreshToken = user.RefreshToken.Token;
			authenticatedUser.UserRole = user.UserRoles.FirstOrDefault()?.Role?.Name.ToString();
			return authenticatedUser;
		}

		public IEnumerable<RoleResource> GetUserRolesAsync(Guid userId)
		{
			var roles = _userRepository.GetRolesForUser(userId);
			return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(roles);
		}

		public async Task<AuthenticatedUserModel> ValidateRefreshToken(Guid userId, string refreshToken)
		{
			User user = await _userRepository.GetByIdAsync(userId);
			// Check there is a user with this refresh token
			if (user == null || user.RefreshToken.Token != refreshToken)
			{
				throw new BaseException(400, "Access or refresh token is not correct.");
			}

			// Check if refresh token has expired
			if (user.RefreshToken.Expiration < DateTime.Now)
			{
				throw new BaseException(400, "Refresh token has expired");
			}
			else
			{
				user = await AssingRefreshTokenToUser(user);
				AuthenticatedUserModel authenticatedUser = _mapper.Map<User, AuthenticatedUserModel>(user);
				authenticatedUser.RefreshToken = user.RefreshToken.Token;
				return authenticatedUser;
			}
		}

		public async Task LogOut(Guid userId)
		{
			User user = await _userRepository.GetByIdAsync(userId);
			user.RefreshToken.Expiration = DateTime.Now;
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<UserStatsResource> GetUserStats(Guid userId)
		{
			User user = await _userRepository.GetByIdAsync(userId);
			UserStatsResource stats = new UserStatsResource();
			stats.UploadNo = user.UploadNo;
			stats.DownloadNo = user.DownloadNo;
			stats.TotalFiles = await _userRepository.GetNumberOfActivefilesForUser(userId);
			return stats;
		}

		public async Task<IEnumerable<UserResource>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllAsync();
			var usersResource = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
			return usersResource;
		}

		public async Task<UserResource> AddUserAsync(UserRequest userData)
		{
			// check if everything is ok
			User user = await _userRepository.GetByUsernameAsync(userData.Username);
			if (user != null)
			{
				// There is a user with this Username already
				throw new BaseException(409, "There is alredy a user with this Username.");
			}

			user = await _userRepository.GetByEmailAsync(userData.Email);
			if (user != null)
			{
				// There is a user with this Email already
				throw new BaseException(409, "There is already a user with this Email.");
			}

			// Everything is OK
			user = _mapper.Map<UserRequest, User>(userData);
			user.Password = Helper.ComputeHash(user.Password);
			user.IsActive = true;
			// Add role
			user.UserRoles = new List<UserRoles>
			{
				new UserRoles{User = user, RoleId = userData.RoleID}
			};

			await _userRepository.AddAsync(user);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<User, UserResource>(user);
		}

		private string GenerateRefreshToken()
		{
			byte[] randomNumber = new byte[32];
			using (var randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		private async Task<User> AssingRefreshTokenToUser(User user)
		{
			user.RefreshToken.Token = GenerateRefreshToken();
			user.RefreshToken.Expiration = DateTime.Now.AddMinutes(5);

			_userRepository.Update(user);
			await _unitOfWork.SaveChangesAsync();

			return user;
		}
	}
}
