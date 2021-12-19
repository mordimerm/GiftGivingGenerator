using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Get;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

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
	
	[HttpPut ("{eventId}/Attendees")]
	public ActionResult AssignPersonsToEvent([FromRoute] Guid eventId, [FromBody] GetIds get)
	{
		var even = _dbContext.Events
			.Include(x=>x.Persons)
			.Single(x => x.Id == eventId);
		
		//TODO: remove all old attendees and then add new getting from json

		foreach (var personId in get.Ids)
		{
			var person = _dbContext.Persons
				.Single(x => x.Id == personId);
			
			even.Persons.Add(person);
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
	
	// ----------------------- GENERATOR ----------------------- //
	
	[HttpPost ("{eventId}/Generate")]
	public ActionResult Generate([FromRoute] Guid eventId)
	{
		var even = _dbContext.Events
			.Include(x=>x.Persons)
			.Single(x => x.Id == eventId);
	
		var personIds = even.Persons
			.Select(x=>x.Id)
			.ToList();
	
		var permutationA = MoreEnumerable.Shuffle(personIds).ToList();
		var permutationB = new List<Guid>();

		var i = 0;
		do
		{
			permutationB = MoreEnumerable.Shuffle(personIds).ToList();
			for (i = 0; i < permutationA.Count; i++)
			{
				Console.WriteLine($"{permutationA[i]} {permutationB[i]}");
				if (permutationA[i] == permutationB[i])
				{
					break;
				}
			}
		}
		while (permutationA.Count != i);
	
		i = 0;
		var results = new List<DrawingResultDto>();
		foreach (var giverId in permutationA)
		{
			var drawingResult = new DrawingResult()
			{
				EventId = eventId,
				GiverPersonId = giverId,
				RecipientPersonId = permutationB[i],
			};
			_dbContext.DrawingResults.Add(drawingResult);

			var result = new DrawingResultDto()
			{
				GiverPersonId = giverId,
				GiverPersonName = _dbContext
					.Persons
					.Single(x=>x.Id== giverId)
					.Name,
				RecipientPersonId = permutationB[i],
				RecipientPersonName = _dbContext
					.Persons
					.Single(x=>x.Id== permutationB[i])
					.Name,
			};
			results.Add(result);
			
			i++;
		}
		_dbContext.SaveChanges();
	
		return Ok(results);
	}
	
	[HttpGet ("{eventId}/Generate/{personId}")]
	public ActionResult Generate([FromRoute] Guid eventId, [FromRoute] Guid personId)
	{
		var drawingResult = _dbContext
			.DrawingResults
			.Single(x => x.EventId == eventId && x.GiverPersonId == personId);

		var recipientPerson = new PersonDto()
		{
			Id = drawingResult.RecipientPersonId,
			Name = _dbContext
				.Persons
				.Single(x=>x.Id==drawingResult.RecipientPersonId)
				.Name,
		};
		
		return Ok(recipientPerson);
	}
}