using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
	[HttpGet]
	public ActionResult<IEnumerable<Event>> GetAllEvents()
	{
		var dbContext = new AppContext();
		var events = dbContext.Events
			.ToList();

		return Ok(events);
	}
}