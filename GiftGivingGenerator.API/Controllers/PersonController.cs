using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Get;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public PersonController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}
	[HttpPost]
	public ActionResult CreatePerson([FromBody] CreateEventDto get)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		
		var person = new Person()
		{
			Name = get.Name
		};
		
		_dbContext.Add(person);
		_dbContext.SaveChanges();

		return Created($"{person.Id}", null);
	}
	
	//TODO: probablly we can't create 2 persons with the same name
	[HttpGet]
	public ActionResult GetPerson([FromBody] GetId get)
	{
		var person = _dbContext
			.Persons
			.Single(x => x.Id == get.Id);

		var personDto = new PersonDto()
		{
			Id = person.Id,
			Name = person.Name,
		};

		return Ok(personDto);
	}

	[HttpPut]
	public ActionResult EditPerson([FromBody] PersonDto person)
	{
		var selectedPerson = _dbContext
			.Persons
			.Single(x => x.Id == person.Id);

		selectedPerson.Name = person.Name;
		_dbContext.Update(selectedPerson);
		_dbContext.SaveChanges();

		return Ok();
	}
	
	//TODO: what should happen with active/no active allocations, when person is removed?
	[HttpDelete]
	public ActionResult DeletePerson([FromBody] GetId get)
	{
		var person = _dbContext
			.Persons
			.Single(x => x.Id == get.Id);

		_dbContext.Remove(person);
		_dbContext.SaveChanges();

		return NoContent();
	}
}