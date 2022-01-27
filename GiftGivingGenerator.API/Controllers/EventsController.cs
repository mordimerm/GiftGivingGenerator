using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
	private readonly IEventRepository _repository;
	private readonly IPersonRepository _personRepository;
	public EventsController(IEventRepository repository, IPersonRepository personRepository)
	{
		_repository = repository;
		_personRepository = personRepository;
	}

	[HttpPost("/Organizers/{organizerId}/Events")]
	public ActionResult CreateEvent([FromRoute] Guid organizerId, [FromBody] CreateEventDto dto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @event = Event.Create(organizerId, dto.Name, dto.EndDate);

		var eventId = _repository.Insert(@event);
		return CreatedAtAction(nameof(GetEventWithPersons), new {id = @eventId}, null);
	}

	[HttpPost("{id}/Exclusions")]
	public ActionResult CreateExclusions([FromRoute]Guid id, [FromBody] List<ExclusionsDto> dto)
	{
		 var @event = _repository.Get(id);
		 @event.InsertExclusions(dto);
		_repository.Update(@event);
		
		return Ok();
	}
	
	/*
	[
	{
		"personId": "7B0DC244-791B-455C-0844-08D9DA682F14",
		"excludedId": [
		"ADF70164-0785-43D4-0846-08D9DA682F14",
		"5D307968-46E8-47E5-0849-08D9DA682F14"
			]
	},


	{
		"personId": "5D307968-46E8-47E5-0849-08D9DA682F14",
		"excludedId": [
		"75641FA5-483C-4CD9-084B-08D9DA682F14",
		"5D307968-46E8-47E5-0849-08D9DA682F14"
			]
	},
	]
*/
	[HttpGet("/Organizers/{organizerId}/Events")]
	public ActionResult<IEnumerable<Event>> GetEventsByOrganizerId([FromRoute] Guid organizerId, bool? isActive, bool? isEndDateExpired)
	{
		var eventsDto = _repository.GetEventsByOrganizerId(organizerId, isActive, isEndDateExpired);

		return Ok(eventsDto);
	}

	[HttpGet("{id}")]
	public ActionResult GetEventWithPersons([FromRoute] Guid id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @eventDto = _repository.Get<EventWithPersonsDto>(id);
		return Ok(@eventDto);
	}

	[HttpPut("{id}/Edit")]
	public ActionResult Edit([FromRoute] Guid id, [FromBody] EditEventDto dto)
	{
		var @event = _repository.Get(id);

		@event.ChangeName(dto.Name);
		@event.ChangeEndDate(dto.Date);
		@event.ChangeMessage(dto.Message);
		@event.ChangeBudget(dto.Budget);

		_repository.Update(@event);
		return Ok();
	}

	[HttpPut("{id}/Attendees")]
	public ActionResult AssignPersonsToEvent([FromRoute] Guid id, [FromBody] PersonsIds dto)
	{
		var @event = _repository.Get(id);
		var persons = _personRepository.GetAllByIds(dto.Ids);
		@event.AssignAttendees(persons);

		_repository.Update(@event);
		return Ok();
	}

	[HttpDelete("{id}")]
	public ActionResult Deactivate([FromRoute] Guid id)
	{
		var @event = _repository.Get(id);
		@event.Deactivate();

		_repository.Update(@event);
		return NoContent();
	}
}