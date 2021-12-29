using GiftGivingGenerator.API.DataTransferObject.Get;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
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

	[HttpPost("/Organizers/{organizerId}/[controller]")]
	public ActionResult CreatePerson([FromRoute] Guid organizerId, [FromBody] CreatePersonDto get)
	{
		var person = Person.Create(get.Name, organizerId);
		
		var personId = _repository.Insert(person);
		return CreatedAtAction(nameof(CreatePerson), new {id = personId}, null);
	}

	[HttpGet("/Organizers/{organizerId}/Persons")]
	public ActionResult<List<PersonDto>> GetPersonsByOrganizer([FromRoute] Guid organizerId)
	{
		var personsDto = _repository.GetPersonsByOrganizer(organizerId);
		
		return Ok(personsDto);
	}

	[HttpPut("{id}/Name")]
	public ActionResult ChangePersonName([FromRoute] Guid id, [FromBody] EditPersonDto get)
	{
		var person = _repository.Get(id);
		person.ChangeName(get.Name);

		_repository.Update(person);
		return Ok(person);
	}

	[HttpDelete("{id}")]
	public ActionResult DeactivatePerson([FromRoute] Guid id)
	{
		var person = _repository.Get(id);
		person.Deactivate();

		_repository.Update(person);
		return NoContent();
	}
}