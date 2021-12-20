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

	[HttpPost ("{organizerId}")]
	public ActionResult CreatePerson([FromRoute] Guid organizerId, [FromBody] GetName get) 
		//TODO: probablly we don't want to create 2 persons with the same name
	{
		var person = new Person()
		{
			Name = get.Name,
			OrganizerId = organizerId,
		};

		_dbContext.Add(person);
		_dbContext.SaveChanges();

		return Created($"{person.Id}", null);
	}

	[HttpGet ("{organizerId}")]
	public ActionResult<List<PersonDto>> GetAllPersons([FromRoute] Guid organizerId)
	{
		var personsDto = _dbContext
			.Persons
			.Where(x=>x.OrganizerId==organizerId)
			.ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
			.ToList();

		return Ok(personsDto);
	}

	[HttpPut("{id}")]
	public ActionResult EditPerson([FromRoute] Guid id, [FromBody] GetName get)
	{
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
}