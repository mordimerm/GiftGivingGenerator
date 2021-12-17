using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public EventController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	[HttpPost]
	public ActionResult CreateEvent([FromBody] EventDto get)
	{
		var even = _mapper.Map<EventDto, Event>(get);

		_dbContext.Add(even);
		_dbContext.SaveChanges();

		return Created($"{even.Id}", null);
	}

	[HttpGet("{id}")]
	public ActionResult GetOneEvent([FromRoute] Guid id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var even = _dbContext.Events
			.ProjectTo<EventWithPersonsDto>(_mapper.ConfigurationProvider)
			.Single(x => x.Id == id);
		
		return Ok(even);
	}

	[HttpGet]
	//TODO? it gets events with list of persons - want we it in this place?
	public ActionResult<IEnumerable<Event>> GetAllEvents()
	{
		var events = _dbContext.Events
			.ProjectTo<EventWithPersonsDto>(_mapper.ConfigurationProvider)
			.ToList();

		return Ok(events);
	}

	[HttpPut("{id}")]
	public ActionResult EditEvent([FromRoute] Guid id, [FromBody] EventDto get)
	{
		var even = _dbContext.Events
			.Single(x => x.Id == id);

		if (get.Name != "")
		{
			even.Name = get.Name;
		}

		//Paulina: ustalić, czy przekaże mi starą datę, czy ja mam to sprawdzać?
		if (get.EndDate != null)
		{
			even.EndDate = get.EndDate;
		}

		_dbContext.Update(even);
		_dbContext.SaveChanges();

		return Ok();
	}

	[HttpDelete("{id}")]
	public ActionResult DeleteEvent([FromRoute] Guid id)
	{
		var even = _dbContext.Events
			.Single(x => x.Id == id);

		_dbContext.Remove(even);
		_dbContext.SaveChanges();

		return NoContent();
	}
}