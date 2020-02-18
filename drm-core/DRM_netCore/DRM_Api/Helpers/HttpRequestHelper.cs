using System;
using System.Security.Claims;
using DRM_Api.Services.Helpers;
using Microsoft.AspNetCore.Http;

namespace DRM_Api.Helpers
{
	public static class HttpRequestHelper
	{
		public static Guid GetUserId(HttpContext context)
		{
			string userIdString = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (!string.IsNullOrEmpty(userIdString))
			{
				return new Guid(userIdString);
			}
			else
			{
				throw new BaseException(500, "Could not resolve userId.");
			}
		}
	}
}