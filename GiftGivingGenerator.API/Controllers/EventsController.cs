using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Exclusion;
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
	private readonly IMailService _mailService;
	private readonly AppSettings _settings;
	public EventsController(IEventRepository eventRepository, IPersonRepository personRepository, IMailService mailService, IOptionsMonitor<AppSettings> settings)
	{
		_eventRepository = eventRepository;
		_personRepository = personRepository;
		_mailService = mailService;
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
		var @event = Event.Create(organizer, dto.Name, dto.EndDate, dto.Budget, dto.Message);

		var listOfDuplicates = dto.Persons.GroupBy(x => x.Name)
			.Where(x => x.Count() > 1)
			.Select(x=>x.Key);

		var duplicates = string.Join(", ", listOfDuplicates);

		if (listOfDuplicates.Any())
		{
			return Conflict($"There are persons with the same names: {duplicates}.");
		}
		
		foreach (var personDto in dto.Persons)
		{
			var person = Person.Create(personDto.Name, personDto.Email);
			@event.Persons.Add(person);
		}

		var eventId = _eventRepository.Insert(@event);
		var eventWithOrganizerDto = _eventRepository.Get<EventDto>(eventId);
		
		return CreatedAtAction(nameof(CreateEvent), new {id = eventId}, eventWithOrganizerDto);
	}

	[HttpPut("{id}/Exclusions")]
	public ActionResult UpdateExclusions([FromRoute]Guid id, [FromBody] List<ListOfExclusionsForOnePersonDto> dto)
	{
		 var @event = _eventRepository.Get(id);
		 @event.UpdateExclusions(dto);
		_eventRepository.Update(@event);
		
		return Ok();
	}
	
	[HttpGet("{id}")]
	public ActionResult GetEventWithPersonsAndExclusions([FromRoute] Guid id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @eventDto = _eventRepository.Get<EventDto>(id);
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

	[HttpPost("{id}/Attendees")]
	public ActionResult AddPersonsToEvent([FromRoute] Guid id, [FromBody] List<CreatePersonDto> dto)
	{
		var @event = _eventRepository.Get(id);
		
		foreach (var personDto in dto)
		{
			var person = Person.Create(personDto.Name, personDto.Email);
			@event.Persons.Add(person);
		}

		_eventRepository.Update(@event);
		return Ok();
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

		var body = $@"<p>Hello {organizer.Name},</p>
						
						<p>
						you created event {@event.Name}.
						<br>Go <a href={_settings.WebApplicationUrl}/Events/{id}><b>link</b></a> to view more details.
						</p>

						<p>
						Best wishes
						<br>GiftGivingGenerator
						</p>";

		_mailService.Send($"{organizer.Email}", $"Links to drawing results '{@event.Name}'", $"{body}");

		return Ok();
	}
}