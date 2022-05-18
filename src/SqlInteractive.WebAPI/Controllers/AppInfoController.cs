using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace SqlInteractive.WebAPI.Controllers;

[ApiController]
[Route("app-info")]
public class VersionController : ControllerBase
{
    private readonly ILogger<VersionController> _logger;

    public VersionController(ILogger<VersionController> logger)
    {
        _logger = logger;
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
		return Ok(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);
	}
}
