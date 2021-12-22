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

	[HttpPut("{eventId}/EditName")]
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

	[HttpPut("{eventId}/EditEndDate")]
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

	// [HttpDelete("{id}")]
	// public ActionResult DeleteEvent([FromRoute] Guid id)
	// {
	// 	var @event = _dbContext.Events
	// 		.Single(x => x.Id == id);
	//
	// 	_dbContext.Remove(@event);
	// 	_dbContext.SaveChanges();
	//
	// 	return NoContent();
	// }

	// // ----------------------- GENERATOR ----------------------- //
	//
	// [HttpPost ("{eventId}/Generate")]
	// public ActionResult Generate([FromRoute] Guid eventId)
	// {
	// 	var @event = _dbContext.Events
	// 		.Include(x=>x.Persons)
	// 		.Single(x => x.Id == eventId);
	//
	// 	var personIds = @event.Persons
	// 		.Select(x=>x.Id)
	// 		.ToList();
	//
	// 	var permutationA = MoreEnumerable.Shuffle(personIds).ToList();
	// 	var permutationB = new List<Guid>();
	//
	// 	var i = 0;
	// 	do
	// 	{
	// 		permutationB = MoreEnumerable.Shuffle(personIds).ToList();
	// 		for (i = 0; i < permutationA.Count; i++)
	// 		{
	// 			Console.WriteLine($"{permutationA[i]} {permutationB[i]}");
	// 			if (permutationA[i] == permutationB[i])
	// 			{
	// 				break;
	// 			}
	// 		}
	// 	}
	// 	while (permutationA.Count != i);
	//
	// 	i = 0;
	// 	var results = new List<DrawingResultDto>();
	// 	foreach (var giverId in permutationA)
	// 	{
	// 		var drawingResult = new DrawingResult()
	// 		{
	// 			EventId = eventId,
	// 			GiverPersonId = giverId,
	// 			RecipientPersonId = permutationB[i],
	// 		};
	// 		_dbContext.DrawingResults.Add(drawingResult);
	//
	// 		var result = new DrawingResultDto()
	// 		{
	// 			GiverPersonId = giverId,
	// 			GiverPersonName = _dbContext
	// 				.Persons
	// 				.Single(x=>x.Id== giverId)
	// 				.Name,
	// 			RecipientPersonId = permutationB[i],
	// 			RecipientPersonName = _dbContext
	// 				.Persons
	// 				.Single(x=>x.Id== permutationB[i])
	// 				.Name,
	// 		};
	// 		results.Add(result);
	// 		
	// 		i++;
	// 	}
	// 	_dbContext.SaveChanges();
	//
	// 	return Ok(results);
	// }
	//
	// [HttpGet ("{eventId}/Generate/{personId}")]
	// public ActionResult Generate([FromRoute] Guid eventId, [FromRoute] Guid personId)
	// {
	// 	var drawingResult = _dbContext
	// 		.DrawingResults
	// 		.Single(x => x.EventId == eventId && x.GiverPersonId == personId);
	//
	// 	var recipientPerson = new PersonDto()
	// 	{
	// 		Name = _dbContext
	// 			.Persons
	// 			.Single(x=>x.Id==drawingResult.RecipientPersonId)
	// 			.Name,
	// 	};
	// 	
	// 	return Ok(recipientPerson);
	// }
}