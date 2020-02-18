using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DRM_Api.Core.Entities.DTO;
using DRM_Api.Core.Repositories;
using DRM_Api.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
	    private readonly IRoleService _roleService;
	    private readonly ILogger<RolesController> _logger;

	    public RolesController(IRoleService roleService, ILogger<RolesController> logger)
	    {
		    _roleService = roleService;
		    _logger = logger;
	    }

	    [HttpGet]
	    [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
	    public async Task<IActionResult> GetAll()
	    {
			try
			{
				return Ok(await _roleService.GetAllAsync());
			}
			catch (Exception ex)
			{
				_logger.LogError("Internal error. " + ex.Message);
				return StatusCode(500, ex.Message);
			}
	    }
    }
}