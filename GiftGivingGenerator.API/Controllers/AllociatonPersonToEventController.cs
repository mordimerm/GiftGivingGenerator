using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AllociatonPersonToEventController : ControllerBase
{
	private readonly AppContext _dbContext;
	public AllociatonPersonToEventController(AppContext dbContext)
	{
		_dbContext = dbContext;
	}
	
	[HttpPut ("{id}")]
	public ActionResult AddPersonsToEvent([FromRoute] Guid id, [FromBody] GetIds get)
	{
		var even = _dbContext.Events
			.Include(x=>x.Persons)
			.Single(x => x.Id == id);

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
}