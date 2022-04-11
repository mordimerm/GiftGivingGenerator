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
	private readonly IDrawingResultRepository _drawingResultRepository;
	private readonly IEventRepository _eventRepository;
	private readonly AppSettings _settings;
	private readonly IMailService _mailService;

	public DrawingResultsController(IDrawingResultRepository drawingResultRepository, IEventRepository eventRepository, IOptionsMonitor<AppSettings> settings, IMailService mailService)
	{
		_drawingResultRepository = drawingResultRepository;
		_eventRepository = eventRepository;
		_settings = settings.CurrentValue;
		_mailService = mailService;
	}

	[HttpPost("/Events/{eventId}/DrawingResults")]
	public ActionResult GenerateDrawingResults([FromRoute] Guid eventId)
	{
		var @event = _eventRepository.Get(eventId);
		var numberOfTries = @event.DrawResultsAndNumberTries();
		Log.Information($"For event {eventId} I trie {numberOfTries} times to draw result.");
		_eventRepository.Update(@event);
		
		var drawingResults = @event.DrawingResults
			.Where(x => x.GiverPerson.Email != null);
		
		foreach (var drawingResult in drawingResults)
		{
			var body = $@"<p>Hello {drawingResult.GiverPerson.Name},</p>
							
							<p>
							{@event.Organizer.Name} created event {@event.Name}.
							<br>Go <a href={_settings.WebApplicationUrl}/drawing-results/{drawingResult.Id}><b>link</b></a> to:
							</p>

							<ul>
								<li>view your drawing result,</li>
								<li>write your gift wish,</li>
								<li>read your recipient's gift wish.</li>
							</ul>

							<p>
							Best wishes
							<br>GiftGivingGenerator
							</p>";

			_mailService.Send($"{drawingResult.GiverPerson.Email}", $"Links to drawing result '{@event.Name}'", $"{body}");
		}

		
		return Ok();
	}

	[HttpGet("/Events/{eventId}/DrawingResults")]
	public ActionResult<List<DrawingResultsForOrganizerDto>> GetAllForEvent([FromRoute] Guid eventId)
	{
		var drawingResults = _drawingResultRepository.GetByEvent<DrawingResultsForOrganizerDto>(eventId);
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