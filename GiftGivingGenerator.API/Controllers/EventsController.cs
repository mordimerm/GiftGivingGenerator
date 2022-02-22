using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
	private readonly IEventRepository _eventRepository;
	private readonly IPersonRepository _personRepository;
	private readonly IMailService _mail;
	private readonly AppSettings _settings;
	public EventsController(IEventRepository eventRepository, IPersonRepository personRepository, IMailService mail, IOptionsMonitor<AppSettings> settings)
	{
		_eventRepository = eventRepository;
		_personRepository = personRepository;
		_mail = mail;
		_settings = settings.CurrentValue;
	}

	[HttpPost]
	public ActionResult CreateEvent([FromBody] CreateEventWithPersonsDto dto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var organizer = Person.Create(dto.OrganizerName, dto.OrganizerEmail);
		var @event = Event.Create(organizer, dto.EventName, dto.EndDate, dto.Budget, dto.Message);
		foreach (var personDto in dto.Persons)
		{
			var person = Person.Create(personDto.Name, personDto.Email);
			@event.Persons.Add(person);
		}

		var eventId = _eventRepository.Insert(@event);
		
		return CreatedAtAction(nameof(GetEventWithPersons), new {id = @eventId}, null);
	}

	[HttpPut("{id}/Exclusions")]
	public ActionResult CreateExclusions([FromRoute]Guid id, [FromBody] List<ExclusionsDto> dto)
	{
		 var @event = _eventRepository.Get(id);
		 @event.InsertExclusions(dto);
		_eventRepository.Update(@event);
		
		return Ok();
	}
	
	[HttpGet("/Organizers/{organizerId}/Events")]
	public ActionResult<IEnumerable<Event>> GetEventsByOrganizerId([FromRoute] Guid organizerId, bool? isActive, bool? isEndDateExpired)
	{
		var eventsDto = _eventRepository.GetByOrganizerId(organizerId, isActive, isEndDateExpired);

		return Ok(eventsDto);
	}

	[HttpGet("{id}")]
	public ActionResult GetEventWithPersons([FromRoute] Guid id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @eventDto = _eventRepository.Get<EventWithPersonsDto>(id);
		return Ok(@eventDto);
	}

	[HttpPut("{id}/Edit")]
	public ActionResult Edit([FromRoute] Guid id, [FromBody] EditEventDto dto)
	{
		var @event = _eventRepository.Get(id);

		@event.ChangeName(dto.Name);
		@event.ChangeEndDate(dto.Date);
		@event.ChangeMessage(dto.Message);
		@event.ChangeBudget(dto.Budget);

		_eventRepository.Update(@event);
		return Ok();
	}

	[HttpPut("{id}/Attendees")]
	public ActionResult AssignPersonsToEvent([FromRoute] Guid id, [FromBody] PersonsIds dto)
	{
		var @event = _eventRepository.Get(id);
		var persons = _personRepository.GetAllByIds(dto.Ids);
		@event.AssignAttendees(persons);

		_eventRepository.Update(@event);
		return Ok();
	}

	[HttpDelete("{id}")]
	public ActionResult Deactivate([FromRoute] Guid id)
	{
		var @event = _eventRepository.Get(id);
		@event.Deactivate();

		_eventRepository.Update(@event);
		return NoContent();
	}
	
	[HttpPost("/{id}/SendMail")]
	public ActionResult SendEmail([FromRoute] Guid id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @event = _eventRepository.Get<EventToSendEmailDto>(id);
		var organizer = _personRepository.Get<OrganizerToSendEmailDto>(@event.OrganizerId);

		var body = $"Hello {organizer.Name}," +
		           $"<br>" +
		           $"<br>you created event {@event.Name}." +
		           $"<br>Go <a href=\"{_settings.WebApplicationUrl}/Events/{id}\"><b>link</b></a> to view more details." +
		           $"<br>" +
		           $"<br>Best wishes" +
		           $"<br>GiftGivingGenerator";

		_mail.Send($"{organizer.Email}", $"Links to drawing results '{@event.Name}'", $"{body}");

		return Ok();
	}
}