using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
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
	private readonly IPersonRepository _personRepository;
	private readonly IDrawingResultRepository _drawingResultRepository;
	private readonly IEventRepository _eventRepository;
	private readonly AppSettings _settings;
	private readonly IMailService _mail;

	public DrawingResultsController(IPersonRepository personRepository, IDrawingResultRepository drawingResultRepository, IEventRepository eventRepository, IOptionsMonitor<AppSettings> settings, IMailService mail)
	{
		_personRepository = personRepository;
		_drawingResultRepository = drawingResultRepository;
		_eventRepository = eventRepository;
		_settings = settings.CurrentValue;
		_mail = mail;
	}

	[HttpPost("/Events/{eventId}/DrawingResults")]
	public ActionResult GenerateDrawingResultsAndSendEmailToEachPerson([FromRoute] Guid eventId)
	{
		var @event = _eventRepository.Get(eventId);
		var numberOfTries = @event.DrawResultsAndNumberTries();
		Log.Information($"For event {eventId} I trie {numberOfTries} times to draw result.");
		_eventRepository.Update(@event);
		
		var organizer = _personRepository.Get(@event.OrganizerId);
		var personsWithEmail = @event.Persons
			.Where(x=>x.Email!=null);
		
		foreach (var person in personsWithEmail)
		{
			var id = _drawingResultRepository.GetByPerson(person.Id).Id;
			Console.WriteLine(person.Name);
			Console.WriteLine(organizer.Name);
			Console.WriteLine(@event.Name);

			var body = $@"Hello {person.Name},
							<br>
							<br>{@event.Organizer.Name} created event {@event.Name}.
							<br>Go <a href={_settings.WebApplicationUrl}/DrawingResults/{id}><b>link</b></a> to:
							<ul>
								<li>view your drawing result,</li>
								<li>write your gift wish,</li>
								<li>read your recipient's gift wish.</li>
								</ul>
							<br>Best wishes
							<br>GiftGivingGenerator";

			_mail.Send($"{person.Email}", $"Links to drawing result '{@event.Name}'", $"{body}");
		}

		
		return Ok();
	}

	[HttpGet("/Events/{eventId}/DrawingResults")]
	public ActionResult<List<DrawingResultsForOrganizerDto>> GetAllForEvent([FromRoute] Guid eventId)
	{
		var drawingResults = _drawingResultRepository.GetDrawingResultsByEventId(eventId);
		if (!drawingResults.Any())
		{
			return NotFound();
		}
		else
		{
			return Ok(drawingResults);
		}
	}

	[HttpGet("{id}")]
	public ActionResult<DrawingResultForUserDto> Get([FromRoute] Guid id)
	{
		return Ok(_drawingResultRepository.Get<DrawingResultForUserDto>(id));
	}
}