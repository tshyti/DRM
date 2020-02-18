using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DRM_Api.Core.Entities;
using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Services;
using DRM_Api.Extensions;
using DRM_Api.Helpers;
using DRM_Api.Services.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace DRM_Api.Controllers
{
	/// <summary>
	/// Used for user authentication and authorization
	/// </summary>
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
	    private readonly IUserService _userService;
	    private readonly TokenAppSettings _tokenGenerationOptions;
	    private readonly ILogger<AuthenticationController> _logger;
	    private readonly IAccessTokenManager _accessTokenManager;

	    public AuthenticationController(IUserService userService, IOptions<TokenAppSettings> tokenGenerationOptions,
		    ILogger<AuthenticationController> logger, IAccessTokenManager accessTokenManager)
	    {
		    _userService = userService;
		    _tokenGenerationOptions = tokenGenerationOptions.Value;
		    _logger = logger;
		    _accessTokenManager = accessTokenManager;
	    }

		/// <summary>
		/// Authenticates user
		/// </summary>
		[AllowAnonymous]
	    [HttpPost("Login")]
		[ProducesResponseType(typeof(AuthenticatedUserModel), 200)]
	    public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel authenticateModel)
	    {
		    if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }

		    try
		    {
			    var user = await _userService.AuthenticateAsync(authenticateModel.Username, authenticateModel.Password);

			    if (user == null)
			    {
				    return BadRequest("Username or password is incorrect.");
			    }

			    var tokenResponse = GenerateJWT(user);
			    user.Token = tokenResponse.Token;
			    user.ExpirationTime = tokenResponse.ExpirationDate;

			    return Ok(user);
		    }
		    catch (BaseException ex)
		    {
			    return BadRequest(ex.Message);
		    }
		    catch (Exception ex)
		    {
			    // Logging
				_logger.LogError(ex,"Internal error.");
			    return StatusCode(500, "Internal error.\n " + ex.Message);
		    }
	    }

	    [AllowAnonymous]
		[HttpPost("Refresh")]
	    [ProducesResponseType(typeof(AuthenticatedUserModel), 200)]
	    public async Task<IActionResult> Refresh([FromBody] TokenRefreshRequest refreshRequest)
	    {
		    if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }

		    try
		    {
			    Guid userId = GetUserIdFromToken(refreshRequest.AccessToken);
			    var authenticatedUser = await _userService.ValidateRefreshToken(userId, refreshRequest.RefreshToken);
			    var tokenResponse = GenerateJWT(authenticatedUser);
			    authenticatedUser.Token = tokenResponse.Token;
			    authenticatedUser.ExpirationTime = tokenResponse.ExpirationDate;
			    return Ok(authenticatedUser);
		    }
		    catch (BaseException baseException)
		    {
			    return BadRequest(baseException.Message);
		    }
		    catch (SecurityTokenException securityTokenException)
		    {
			    return BadRequest(securityTokenException.Message);
		    }
		    catch (Exception ex)
		    {
			    _logger.LogError(ex,"Internal error.");
			    return StatusCode(500, "Internal error");
		    }
	    }

		/// <summary>
		/// Log out of the application
		/// </summary>
		[HttpPost("Logout")]
	    public async Task<IActionResult> LogOut()
	    {
			try
			{
				string accessToken = await HttpContext.GetTokenAsync("access_token");
				Guid userId = GetUserIdFromToken(accessToken);
				//await _accessTokenManager.DeactivateCurrentAccessTokenAsync();
				await _userService.LogOut(userId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex,"Internal error. " + ex.Message);
				return StatusCode(500, "Internal error.");
			}
		    return Ok();
	    }

	    private dynamic GenerateJWT(AuthenticatedUserModel userData)
	    {
		    List<Claim> claims = GetClaims(userData);
		    DateTime expirationDate = DateTime.Now.AddDays(int.Parse(_tokenGenerationOptions.AccessTokenExpiration));

		    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenGenerationOptions.Key));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
					issuer: _tokenGenerationOptions.Issuer,
					audience: _tokenGenerationOptions.Audience,
					expires: expirationDate,
					claims: claims,
					signingCredentials: signingCredentials
				);

			return new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				ExpirationDate = expirationDate
			};
		}

	    private List<Claim> GetClaims(AuthenticatedUserModel userData)
	    {
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, userData.Username),
				new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString())
			};

			try
			{
				var userRoles = _userService.GetUserRolesAsync(userData.Id);
				foreach (var userRole in userRoles)
				{
					claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex,"Could not get claims for user.");
			}

			return claims;
	    }

	    private Guid GetUserIdFromToken(string accessToken)
	    {
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidAudience = _tokenGenerationOptions.Audience,
				ValidateIssuer = true,
				ValidIssuer = _tokenGenerationOptions.Issuer,
				ValidateActor = false,
				ValidateLifetime = false, // This should be false
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenGenerationOptions.Key))
			};

			SecurityToken securityToken;
			var principal = new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidationParameters, out securityToken);
			JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null)
			{
				throw new SecurityTokenException("Invalid token");
			}

			string id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(id))
			{
				throw new SecurityTokenException($"Missing claim {ClaimTypes.NameIdentifier}.");
			}
			
			Guid userID = new Guid(id);
			if (userID != Guid.Empty)
			{
				return userID;
			}
			else
			{
				throw new SecurityTokenException($"Problem with claim {ClaimTypes.NameIdentifier}.");
			}
	    }
    }
}