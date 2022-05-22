using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace SqlInteractive.WebAPI.Controllers;

[ApiController]
[Route("app-info")]
public class AppInfoController : ControllerBase
{
    private readonly ILogger<AppInfoController> _logger;

    public AppInfoController(ILogger<AppInfoController> logger)
    {
        _logger = logger;
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
		return Ok(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);
	}
}
