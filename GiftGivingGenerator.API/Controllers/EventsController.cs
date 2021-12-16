using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.ModelsDataTransferObject;
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
			.Select(x=>new PrintListOfEventsDto()
			{
				Id = x.Id,
				Name = x.Name,
				EndDate = x.EndDate
			})
			.ToList();

		return Ok(events);
	}
}