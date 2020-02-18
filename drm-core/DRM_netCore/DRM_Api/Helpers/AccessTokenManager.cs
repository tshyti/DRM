using System;
using System.Threading.Tasks;
using DRM_Api.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace DRM_Api.Helpers
{
	public class AccessTokenManager : IAccessTokenManager
	{
		private readonly IDistributedCache _cache;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IOptions<TokenAppSettings> _jwtOptions;

		public AccessTokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor, IOptions<TokenAppSettings> jwtOptions)
		{
			_cache = cache;
			_httpContextAccessor = httpContextAccessor;
			_jwtOptions = jwtOptions;
		}

		public async Task<bool> IsCurrentActiveAcessTokenAsync()
		{
			return await IsActiveTokenAsync(await GetCurrentAccessTokenAsync());
		}

		public async Task DeactivateCurrentAccessTokenAsync()
		{
			await DeactivateAccessTokenAsync(await GetCurrentAccessTokenAsync());
		}

		public async Task<bool> IsActiveTokenAsync(string accessToken)
		{
			return await _cache.GetStringAsync(GetKey(accessToken)) == null;
		}

		public async Task DeactivateAccessTokenAsync(string accessToken)
		{
			await _cache.SetStringAsync(GetKey(accessToken), "randomValue", new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(double.Parse(_jwtOptions.Value.AccessTokenExpiration))
			});
		}

		private async Task<string> GetCurrentAccessTokenAsync()
		{
			string authorizationToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
			if (string.IsNullOrEmpty(authorizationToken))
			{
				return string.Empty;
			}
			else
			{
				return authorizationToken;
			}
		}

		private static string GetKey(string token)
		{
			return $"tokens:{token}:deactivated";
		}
	}
}