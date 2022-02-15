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
	private readonly IDrawingResultRepository _repository;
	private readonly IEventRepository _eventRepository;

	public DrawingResultsController(IDrawingResultRepository repository, IEventRepository eventRepository)
	{
		_repository = repository;
		_eventRepository = eventRepository;
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
	public ActionResult<List<DrawingResultsForOrganizerDto>> GetAllForEvent([FromRoute] Guid eventId)
	{
		var drawingResults = _repository.GetDrawingResultsByEventId(eventId);
		if (drawingResults.Count == 0)
		{
			return NotFound();
		}
		else
		{
			return Ok();
		}
	}

	[HttpGet("{id}")]
	public ActionResult<DrawingResultDto> Get([FromRoute] Guid id)
	{
		return Ok(_repository.Get(id));
	}
}