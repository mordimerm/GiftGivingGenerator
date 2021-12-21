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
public class PersonsController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public PersonsController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	[HttpPost ("/{organizerId}/[controller]")]
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

		return CreatedAtAction(nameof(CreatePerson), new {id = person.Id}, person);
	}

	[HttpGet ("/{organizerId}/[controller]")]
	public ActionResult<List<PersonDto>> GetAllPersons([FromRoute] Guid organizerId)
	{
		var personsDto = _dbContext.Persons
			.Where(x=>x.OrganizerId==organizerId)
			.ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
			.ToList();

		return Ok(personsDto);
	}

	[HttpPut("{personId}/EditName")]
	public ActionResult EditPersonName([FromRoute] Guid personId, [FromBody] GetName get)
	{
		var person = _dbContext
			.Persons
			.Single(x => x.Id == personId);

		person.Name = get.Name;
		_dbContext.Update(person);
		_dbContext.SaveChanges();

		return Ok(person);
	}

	//TODO: change to two possibilities:
	//1		delete person if don't have allocations with events and drawing results
	//2		sign as no active if has any allocations
	[HttpDelete("{personId}")]
	public ActionResult DeletePerson([FromRoute] Guid personId)
	{
		var person = _dbContext
			.Persons
			.Single(x => x.Id == personId);

		_dbContext.Remove(person);
		_dbContext.SaveChanges();

		return NoContent();
	}
}