using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Get;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

	[HttpPost("/{organizerId}/[controller]")]
	public ActionResult CreateEvent([FromRoute] Guid organizerId, [FromBody] InputEventDto get)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		if (get.EndDate < DateTime.Now)
		{
			ModelState.AddModelError(nameof(InputEventDto.EndDate), "Date must be later then now.");
			return BadRequest(ModelState);
		}

		var @event = _mapper.Map<InputEventDto, Event>(get);
		@event.OrganizerId = organizerId;
		@event.EndDate = get.EndDate.Date;

		_dbContext.Add(@event);
		_dbContext.SaveChanges();

		return CreatedAtAction(nameof(GetOneEventWithPersons), new {id = @event.Id}, @event);
	}

	[HttpGet("/{organizerId}/[controller]")]
	public ActionResult<IEnumerable<Event>> GetAllActiveEvents([FromRoute] Guid organizerId)
	{
		var events = _dbContext
			.Events
			.Where(x => x.OrganizerId == organizerId)
			.Where(x => x.IsActive == true)
			.ProjectTo<OutputEventDto>(_mapper.ConfigurationProvider)
			.ToList();

		return Ok(events);
	}

	[HttpGet("{eventId}")]
	public ActionResult GetOneEventWithPersons([FromRoute] Guid eventId)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @event = _dbContext
			.Events
			.ProjectTo<EventWithPersonsDto>(_mapper.ConfigurationProvider)
			.Single(x => x.Id == eventId);

		return Ok(@event);
	}

	[HttpPut("{eventId}/Name")]
	public ActionResult EditEventName([FromRoute] Guid eventId, [FromBody] GetName get)
	{
		var @event = _dbContext
			.Events
			.Single(x => x.Id == eventId);

		if (!string.IsNullOrEmpty(get.Name))
		{
			return BadRequest("Name can't be null.");
		}

		@event.Name = get.Name;
		_dbContext.Update(@event);
		_dbContext.SaveChanges();

		return Ok(@event);
	}

	[HttpPut("{eventId}/EndDate")]
	public ActionResult EditEventEndDate([FromRoute] Guid eventId, [FromBody] GetDateTime get)
	{
		var @event = _dbContext
			.Events
			.Single(x => x.Id == eventId);

		if (get.DateTime < DateTime.Now)
		{
			ModelState.AddModelError(nameof(InputEventDto.EndDate), "Date must be later then now.");
			return BadRequest(ModelState);
		}

		_dbContext.Update(@event);
		_dbContext.SaveChanges();

		return Ok(@event);
	}

	[HttpPut("{eventId}/Attendees")]
	public ActionResult AssignPersonsToEvent([FromRoute] Guid eventId, [FromBody] GetIds get)
	{
		var @event = _dbContext.Events
			.Include(x => x.Persons)
			.Single(x => x.Id == eventId);

		@event.Persons.Clear();

		var persons = _dbContext.Persons
			.Where(x => get.Ids.Contains(x.Id))
			.ToList();
		@event.Persons.AddRange(persons);

		_dbContext.Update(@event);
		_dbContext.SaveChanges();

		return Ok(@event);
	}

	[HttpDelete("{id}")]
	public ActionResult SignEventAsNoActive([FromRoute] Guid id)
	{
		var @event = _dbContext.Events
			.Single(x => x.Id == id);

		@event.IsActive = false;

		_dbContext.Update(@event);
		_dbContext.SaveChanges();

		return NoContent();
	}
}