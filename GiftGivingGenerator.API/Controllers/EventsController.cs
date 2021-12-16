using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Events;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public EventsController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}
	[HttpGet]
	public ActionResult<IEnumerable<Event>> GetAllEvents()
	{
		var events = _dbContext.Events
			.Select(x=>new ListOfEventsDto()
			{
				Id = x.Id,
				Name = x.Name,
				EndDate = x.EndDate
			})
			.ToList();

		return Ok(events);
	}
}