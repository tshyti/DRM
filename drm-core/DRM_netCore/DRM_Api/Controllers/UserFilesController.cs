using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Services;
using DRM_Api.Helpers;
using DRM_Api.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace DRM_Api.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserFilesController : ControllerBase
    {
	    private readonly IUserFileService _userFileService;
	    private readonly ILogger<UserFilesController> _logger;

	    public UserFilesController(IUserFileService userFileService, ILogger<UserFilesController> logger)
	    {
		    _userFileService = userFileService;
		    _logger = logger;
	    }

	    [HttpGet]
	    [ProducesResponseType(typeof(IEnumerable<UserFileResource>), 200)]
        public async Task<IActionResult> GetFiles()
        {
	        try
	        {
		        Guid userId = HttpRequestHelper.GetUserId(HttpContext);
		        var files = await _userFileService.GetUserFiles(userId);
		        return Ok(files);
	        }
	        catch (Exception ex)
	        {
				_logger.LogError(ex.Message);
		        return StatusCode(500, ex.Message);
	        }
        }

		[HttpPost]
		[ProducesResponseType(typeof(UserFileResource), 200)]
        public async Task<IActionResult> UploadUserFile([FromBody] UserFileUploadData fileData)
        {
	        if (!ModelState.IsValid)
	        {
		        return BadRequest(ModelState);
	        }

	        try
	        {
		        UserFileResource createdFile =
			        await _userFileService.UploadUserFile(fileData, HttpRequestHelper.GetUserId(HttpContext));
		        return Ok(createdFile);
	        }
	        catch (BaseException baseException)
	        {
				_logger.LogError(baseException.Details);
				return StatusCode(500, "Internal error." + baseException.Message);
	        }
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return StatusCode(500, "Internal error." + ex.Message);
			}
        }

		[HttpGet("{userFileId}")]
		[ProducesResponseType(typeof(UserFileDownloadResource), 200)]
		public async Task<IActionResult> DownloadUserFile(Guid userFileId)
		{
			if (userFileId == null || userFileId == Guid.Empty)
			{
				return BadRequest("Id is not valid.");
			}

			try
			{
				var file = await _userFileService.DownloadUserFile(userFileId,
					HttpRequestHelper.GetUserId(HttpContext));
				return Ok(file);
			}
			catch(BaseException baseException)
			{
				_logger.LogInformation(baseException.Message);
				return StatusCode(baseException.StatusCode, baseException.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{userFileId}")]
		[ProducesResponseType(typeof(UserFileResource), 200)]
		public async Task<IActionResult> DeleteFile(Guid userFileId)
		{
			if (userFileId == null || userFileId == Guid.Empty)
			{
				return BadRequest("File Id is not in the correct format.");
			}

			try
			{
				UserFileResource deletedFile =
					await _userFileService.DeleteUserFile(userFileId, HttpRequestHelper.GetUserId(HttpContext));
				return Ok(deletedFile);
			}
			catch (BaseException baseException)
			{
				return StatusCode(baseException.StatusCode, baseException.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return StatusCode(500, "Internal error.");
			}
		}
	}
}