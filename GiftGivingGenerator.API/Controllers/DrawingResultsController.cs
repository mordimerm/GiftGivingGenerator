using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DrawingResultsController : ControllerBase
{
	private readonly MailConfiguration _options;
	private readonly IDrawingResultRepository _repository;
	private readonly IEventRepository _eventRepository;
	public DrawingResultsController(IOptionsMonitor<MailConfiguration> options, IDrawingResultRepository repository, IEventRepository eventRepository)
	{
		_options = options.CurrentValue;
		_repository = repository;
		_eventRepository = eventRepository;
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
		var userName = _options.userName;
		var password = _options.password;
		var @event = _eventRepository.Get(eventId);
		var drawingResults = _repository.GetDrawingResultsByEventId(eventId).Select(x=>x.Id);
		
		string body="";
		foreach (var drawingResult in drawingResults)
		{
			body += $"http://localhost:5036/DrawingResults/{drawingResult}\n";
		}
		
		new EmailService().Send(
			userName,
			password,
			"aczekaj.mat@gmail.com",
			$"Links to drawing results: {@event.Name}", 
			$"{body}");

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		
		return Ok();
	}
	
	[HttpGet ("{id}")]
	public ActionResult<DrawingResultDto> Get([FromRoute] Guid id)
	{
		return Ok(_repository.Get(id));
	}
}