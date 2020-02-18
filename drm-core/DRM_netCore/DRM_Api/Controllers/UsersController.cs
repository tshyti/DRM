using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Services;
using DRM_Api.Extensions;
using DRM_Api.Helpers;
using DRM_Api.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DRM_Api.Controllers
{
    [Authorize]
	[Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public UsersController(IUserService userService, ILogger<AuthenticationController> logger)
        {
	        _userService = userService;
	        _logger = logger;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns IEnumerable of UserResource models</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch(Exception ex)
            {
	            _logger.LogError(ex,"Internal error.");
	            return StatusCode(500, "Internal error. \n " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResource), 200)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest userData)
        {
            // Is data valid?
            if (!ModelState.IsValid)
            {
	            return BadRequest(ModelState);
            }

            try
            {
                // Call the service if data is valid
                var response = await _userService.AddUserAsync(userData);
                return Ok(response);
            }
            catch (BaseException ex)
            {
                if (ex.StatusCode == 400)
                {
                    return BadRequest(ex.Message);
                }
                else
                {
                    return StatusCode(ex.StatusCode, ex.Message);
                }
            }
            catch (Exception ex)
            {
	            _logger.LogError(ex,"Internal error.", ex.InnerException);
                return StatusCode(500, "Internal error.");
            }
        }

        [HttpGet("GetUserStats")]
        [ProducesResponseType(typeof(UserFileResource), 200)]
        public async Task<IActionResult> GetUserStats()
        {
            try
            {
                return Ok(await _userService.GetUserStats(HttpRequestHelper.GetUserId(HttpContext)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal error");
            }
        }
    }
}