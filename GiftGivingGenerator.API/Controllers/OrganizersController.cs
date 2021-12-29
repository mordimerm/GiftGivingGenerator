using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizersController : ControllerBase
{

	private readonly IOrganizerRepository _repository;
	private readonly IPersonRepository _repositoryPerson;
	public OrganizersController(IOrganizerRepository repository, IPersonRepository repositoryPerson)
	{
		_repository = repository;
		_repositoryPerson = repositoryPerson;
	}

	[HttpPost]
	public ActionResult CreateOrganizer([FromBody] OrganizerDto get)
	{
		var organizer = Organizer.Create(get.Name, get.Email, get.Password);
		var organizerId = _repository.Insert(organizer);
		
		var person = Person.Create(get.Name, organizerId);
		_repositoryPerson.Insert(person);
		
		return CreatedAtAction(nameof (CreateOrganizer), new {id = organizerId}, null);
	}

	[HttpGet]
	public ActionResult GetOrganizers()
	{
		var organizersDto = _repository.GetOrganizers();

		return Ok(organizersDto);
	}

	[HttpGet("{id}")]
	public ActionResult Get([FromRoute] Guid id)
	{
		var organizer = _repository.Get(id);

		return Ok(organizer);
	}
}