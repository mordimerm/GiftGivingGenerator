using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IEventRepository _eventRepository;
	private readonly IPersonRepository _personRepository;
	private readonly IDrawingResultRepository _drawingResultRepository;

	public PersonsController(IEventRepository eventRepository, IPersonRepository personRepository, IDrawingResultRepository drawingResultRepository)
	{
		_eventRepository = eventRepository;
		_personRepository = personRepository;
		_drawingResultRepository = drawingResultRepository;
	}

	[HttpPost]
	public ActionResult Create([FromBody] CreatePersonDto dto)
	{
		var person = Person.Create(dto.Name, dto.Email);
		_personRepository.Insert(person);
		return Created($"/Persons/{person.Id}", person);
	}

	[HttpPut("{id}/Name")]
	public ActionResult ChangePersonName([FromRoute] Guid id, [FromBody] EditPersonDto get)
	{
		var person = _personRepository.Get(id);
		person.ChangeName(get.Name);

		_personRepository.Update(person);
		return Ok();
	}

	[HttpDelete("{id}")]
	public ActionResult DeletePerson(Guid id)
	{
		var drawingResult = _drawingResultRepository.GetByPersonId(id);
		if (drawingResult != null)
		{
			return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
		}
		
		var person = _personRepository.Get(id);
		_personRepository.Delete(person);

		return NoContent();
	}
}