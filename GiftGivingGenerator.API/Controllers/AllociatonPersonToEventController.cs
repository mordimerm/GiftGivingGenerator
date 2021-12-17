using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AllociatonPersonToEventController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public AllociatonPersonToEventController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}
	
	[HttpPut]
	public ActionResult AddPersonsToEvent([FromBody] GetOneIdAndListOfIds get)
	{
		var even = _dbContext.Events
			.Single(x => x.Id == get.Id);

		even.Persons.Clear();

		foreach (var personId in get.Ids)
		{
			var person = _dbContext.Persons
				.Single(x => x.Id == personId);
			
			even.Persons.Add(person);
		}

		_dbContext.Update(even);
		_dbContext.SaveChanges();

		return Ok();
	}

	[HttpDelete]
	public ActionResult RemovePersonsFromEvent([FromBody] GetOneIdAndListOfIds get)
	{
		var even = _dbContext.Events
			.Include(x=>x.Persons)
			.Single(x => x.Id == get.Id);

		foreach (var personId in get.Ids)
		{
			var person = even.Persons
				.SingleOrDefault(x => x.Id == personId);
			even.Persons.Remove(person);
		}
		
		_dbContext.SaveChanges();

		return NoContent();
	}
}