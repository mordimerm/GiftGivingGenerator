using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IPersonRepository _repository;

	public PersonsController(IPersonRepository repository)
	{
		_repository = repository;
	}

	[HttpGet("/Organizers/{organizerId}/Persons")]
	public ActionResult<List<PersonDto>> GetPersonsByOrganizer([FromRoute] Guid organizerId)
	{
		//TODO: write new method to GetPersonsByOrganizer
		//var personsDto = _repository.GetPersonsByOrganizer(organizerId);
		
		return Ok(/*personsDto*/);
	}

	[HttpPut("{id}/Name")]
	public ActionResult ChangePersonName([FromRoute] Guid id, [FromBody] EditPersonDto get)
	{
		var person = _repository.Get(id);
		person.ChangeName(get.Name);

		_repository.Update(person);
		return Ok();
	}
}