using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DrawingResultsController : ControllerBase
{
	private readonly IMailService _mail;
	private readonly IDrawingResultRepository _repository;
	private readonly IEventRepository _eventRepository;
	private readonly IOrganizerRepository _organizerRepository;
	public DrawingResultsController(IMailService mail, IDrawingResultRepository repository, IEventRepository eventRepository, IOrganizerRepository organizerRepository)
	{
		_mail = mail;
		_repository = repository;
		_eventRepository = eventRepository;
		_organizerRepository = organizerRepository;
	}
	
	[HttpPost("/{eventId}/DrawingResults")]
	public ActionResult GenerateDrawingResults([FromRoute] Guid eventId)
	{
		var @event = _eventRepository.Get(eventId);
		@event.DrawResults();
		
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
		//Maciek: it doesn't work - program threw Internal Server Error behind Bad Request
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		
		var @event = _eventRepository.Get(eventId);
		var organizer = _organizerRepository.Get(@event.OrganizerId);
		
		var drawingResultIds = _repository.GetDrawingResultsByEventId(eventId).Select(x=>x.Id);
		var body = "http://localhost:5036/DrawingResults/" + string.Join("\nhttp://localhost:5036/DrawingResults/", drawingResultIds);

		_mail.Send($"{organizer.Email}", $"Links to drawing results '{@event.Name}'", $"{body}");
		
		return Ok();
	}
	
	[HttpGet ("{id}")]
	public ActionResult<DrawingResultDto> Get([FromRoute] Guid id)
	{
		return Ok(_repository.Get(id));
	}
}