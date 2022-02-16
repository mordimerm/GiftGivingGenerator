using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IPersonRepository _personRepository;
	private readonly AppSettings _settings;

	public PersonsController(IPersonRepository personRepository, IOptionsMonitor<AppSettings> settings)
	{
		_personRepository = personRepository;
		_settings = settings.CurrentValue;
	}
	
	[HttpPost] 
	public ActionResult Create([FromBody] List<CreatePersonDto> personsDtos) 
	{ 
		var persons = Person.CreateMany(personsDtos); 
		_personRepository.InsertMany(persons); 
		return Created($"{_settings.WebApplicationUrl}/Persons/", persons); 
	} 

	[HttpPut("{id}/Name")]
	public ActionResult ChangePersonName([FromRoute] Guid id, [FromBody] EditPersonDto get)
	{
		var person = _personRepository.Get(id);
		person.ChangeName(get.Name);

		_personRepository.Update(person);
		return Ok();
	}
}