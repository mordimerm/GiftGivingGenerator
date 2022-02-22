using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IPersonRepository _personRepository;
	private readonly IDrawingResultRepository _drawingResultRepository;

	public PersonsController(IPersonRepository personRepository, IDrawingResultRepository drawingResultRepository)
	{
		_personRepository = personRepository;
		_drawingResultRepository = drawingResultRepository;
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