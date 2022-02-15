using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DrawingResultsController : ControllerBase
{
	private readonly IMailService _mail;
	private readonly IDrawingResultRepository _repository;
	private readonly IEventRepository _eventRepository;
	private readonly IPersonRepository _personRepository;
	private readonly AppSettings _settings;

	public DrawingResultsController(IMailService mail, IDrawingResultRepository repository, IEventRepository eventRepository, IPersonRepository personRepository, IOptionsMonitor<AppSettings> settings)
	{
		_mail = mail;
		_repository = repository;
		_eventRepository = eventRepository;
		_personRepository = personRepository;
		_settings = settings.CurrentValue;
	}

	[HttpPost("/{eventId}/DrawingResults")]
	public ActionResult GenerateDrawingResults([FromRoute] Guid eventId)
	{
		var @event = _eventRepository.Get(eventId);
		var numberOfTries = @event.DrawResultsAndNumberTries();
		Log.Information($"For event {eventId} I trie {numberOfTries} times to draw result.");

		_eventRepository.Update(@event);
		return Ok();
	}

	[HttpGet("/{eventId}/DrawingResults")]
	public ActionResult<List<DrawingResultDto>> GetAllForEvent([FromRoute] Guid eventId)
	{
		return Ok(_repository.GetDrawingResultsByEventId(eventId));
	}

	[HttpPost("/{eventId}/DrawingResults/SendMail")]
	public ActionResult SendEmail([FromRoute] Guid eventId)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var @event = _eventRepository.Get<EventToSendEmailDto>(eventId);
		var organizer = _personRepository.Get<OrganizerToSendEmailDto>(@event.OrganizerId);

		var body = $"Hello {organizer.Name}," +
						$"<br>" +
						$"<br>you created event {@event.Name}." +
						$"<br>Go <a href=\"{_settings.WebApplicationUrl}/Events/{eventId}\"><b>link</b></a> to view more details." +
						$"<br>" +
						$"<br>Best wishes" +
						$"<br>GiftGivingGenerator";

		_mail.Send($"{organizer.Email}", $"Links to drawing results '{@event.Name}'", $"{body}");

		return Ok();
	}

	[HttpGet("{id}")]
	public ActionResult<DrawingResultDto> Get([FromRoute] Guid id)
	{
		return Ok(_repository.Get(id));
	}
}