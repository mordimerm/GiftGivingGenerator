﻿using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Email;
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
	private readonly IEmailService _emailService;

	public DrawingResultsController(IDrawingResultRepository drawingResultRepository, IEventRepository eventRepository, IOptionsMonitor<AppSettings> settings, IEmailService emailService)
	{
		_drawingResultRepository = drawingResultRepository;
		_eventRepository = eventRepository;
		_settings = settings.CurrentValue;
		_emailService = emailService;
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

		var mails = new List<Email>();
		
		foreach (var drawingResult in drawingResults)
		{
			var mail = new Email();
			mail.Recipient = drawingResult.GiverPerson.Email;
			mail.Subject = $"Links to drawing result '{@event.Name}'";
			mail.Body = $@"<p>Hello {drawingResult.GiverPerson.Name},</p>
							
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

			mails.Add(mail);
		}
		
		_emailService.Send(mails);
		
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