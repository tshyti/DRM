using System.Net;
using System.Threading.Tasks;
using DRM_Api.Core.Services;
using Microsoft.AspNetCore.Http;

namespace DRM_Api.Middleware
{
	public class AccessTokenManagerMiddleware : IMiddleware
	{
		private readonly IAccessTokenManager _accessTokenManager;

		public AccessTokenManagerMiddleware(IAccessTokenManager accessTokenManager)
		{
			_accessTokenManager = accessTokenManager;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (await _accessTokenManager.IsCurrentActiveAcessTokenAsync())
			{
				await next(context);
				return;
			}

			context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
		}
	}
}