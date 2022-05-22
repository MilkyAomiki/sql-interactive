using Microsoft.AspNetCore.Mvc;
using SqlInteractive.BLL.Models;

namespace SqlInteractive.WebAPI.Controllers;

public class ApiController : ControllerBase
{
	public const string SESSION_ID_KEY = "sessionId";

	protected Session GetSession()
	{
		string sessionIdStr = HttpContext.Session.GetString(SESSION_ID_KEY);

		Guid sessionId;
		if(sessionIdStr is null)
		{
			sessionId = Guid.NewGuid();
			HttpContext.Session.SetString(SESSION_ID_KEY, sessionId.ToString());
		}
		else
		{
			sessionId = Guid.Parse(sessionIdStr);
		}

		return new Session(sessionId);
	}
}
