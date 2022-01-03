using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IPersonRepository _repository;
	private readonly IOrganizerRepository _organizerRepository;
	private readonly ILogger<PersonsController> _logger;

	public PersonsController(IPersonRepository repository, IOrganizerRepository organizerRepository, ILogger<PersonsController> logger)
	{
		_repository = repository;
		_organizerRepository = organizerRepository;
		_logger = logger;
	}

	[HttpPost("/Organizers/{organizerId}/Persons")]
	public ActionResult CreatePerson([FromRoute] Guid organizerId, [FromBody] CreatePersonDto dto)
	{
		var organizer = _organizerRepository.Get(organizerId);
		var person = organizer.AddPerson(dto.Name);
		_logger.LogInformation($"Added {person.Name}.");
		
		_organizerRepository.Update(organizer);
		return CreatedAtAction(nameof(CreatePerson), new {id = person.Id}, null);
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
		return Ok();
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