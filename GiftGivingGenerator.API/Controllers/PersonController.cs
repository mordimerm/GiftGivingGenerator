using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
	public ActionResult CreatePerson([FromBody] GetName get)
		//TODO: probablly we don't want to create 2 persons with the same name
	{
		var person = new Person()
		{
			Name = get.Name
		};

		_dbContext.Add(person);
		_dbContext.SaveChanges();

		return Created($"{person.Id}", null);
	}

	[HttpGet("{id}")]
	public ActionResult GetPerson([FromRoute] Guid id)
	{
		var personDto = _dbContext
			.Persons
			.ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
			.Single(x => x.Id == id);

		return Ok(personDto);
	}

	[HttpGet]
	public ActionResult<List<PersonDto>> GetAllPersons()
	{
		var personsDto = _dbContext
			.Persons
			.ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
			.ToList();

		return Ok(personsDto);
	}

	[HttpPut("{id}")]
	public ActionResult EditPerson([FromRoute] Guid id, [FromBody] GetName get)
	{
		//tu nie działa string name, bo nie można odnieść się przez format json
		var person = _dbContext
			.Persons
			.Single(x => x.Id == id);

		person.Name = get.Name;
		_dbContext.Update(person);
		_dbContext.SaveChanges();

		return Ok();
	}

	//TODO: what should happen with active/no active allocations, when person is removed?
	[HttpDelete("{id}")]
	public ActionResult DeletePerson([FromRoute] Guid id)
	{
		var person = _dbContext
			.Persons
			.Single(x => x.Id == id);

		_dbContext.Remove(person);
		_dbContext.SaveChanges();

		return NoContent();
	}

	[HttpDelete]
	public ActionResult DeletePerson([FromBody] GetIds get)
	{
		var persons = _dbContext
			.Persons
			.Where(x=> get.Ids.Contains(x.Id))
			.ToList();

		_dbContext.RemoveRange(persons);
		_dbContext.SaveChanges();

		return NoContent();
	}
}