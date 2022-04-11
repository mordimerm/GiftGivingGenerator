using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoreLinq.Experimental;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IPersonRepository _personRepository;
	private readonly IDrawingResultRepository _drawingResultRepository;
	private readonly IMailService _mailService;
	private readonly AppSettings _settings;

	public PersonsController(IPersonRepository personRepository, IDrawingResultRepository drawingResultRepository, IOptionsMonitor<AppSettings> settings, IMailService mailService)
	{
		_personRepository = personRepository;
		_drawingResultRepository = drawingResultRepository;
		_mailService = mailService;
		_settings = settings.CurrentValue;
	}

	[HttpPut("{id}/Name")]
	public ActionResult ChangePersonName([FromRoute] Guid id, [FromBody] EditPersonDto get)
	{
		var person = _personRepository.Get(id);
		person.ChangeName(get.Name);

		_personRepository.Update(person);
		return Ok();
	}
	
	[HttpPost("{id}/Mail")]
	public ActionResult SendEmailWithDrawingResult([FromRoute] Guid id)
	{
		var person = _personRepository.Get<PersonToSendEmailDto>(id);
		var @event = person.Events.First();
		var drawingResult = @event.DrawingResults.SingleOrDefault(x => x.GiverPersonId == id);

		var body = $@"<p>Hello {person.Name},</p>
							
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

		_mailService.Send($"{person.Email}", $"Links to drawing result '{@event.Name}'", $"{body}");
		
		return Ok();
	}

	[HttpDelete("{id}")]
	public ActionResult Delete(Guid id)
	{
		var drawingResult = _drawingResultRepository.GetByPerson(id);
		if (drawingResult != null)
		{
			return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
		}

		_personRepository.Delete(id);
		return NoContent();
	}
}