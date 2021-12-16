using GiftGivingGenerator.API.DataTransferObject;
using GiftGivingGenerator.API.DataTransferObject.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AllociatonPersonToEventController : ControllerBase
{
	[HttpPut]
	public ActionResult AddPersonsToEvent([FromBody] GetOneIdAndListOfIds get)
	{
		var dbContext = new AppContext();
		var even = dbContext.Events
			.Single(x => x.Id == get.Id);

		even.Persons.Clear();

		foreach (var personId in get.Ids)
		{
			var person = dbContext.Persons
				.Single(x => x.Id == personId);
			
			even.Persons.Add(person);
		}

		dbContext.Update(even);
		dbContext.SaveChanges();

		return Ok();
	}

	[HttpDelete]
	public ActionResult RemovePersonsFromEvent([FromBody] GetOneIdAndListOfIds get)
	{
		var dbContext = new AppContext();
		var even = dbContext.Events
			.Include(x=>x.Persons)
			.Single(x => x.Id == get.Id);

		foreach (var personId in get.Ids)
		{
			var person = even.Persons
				.SingleOrDefault(x => x.Id == personId);
			even.Persons.Remove(person);
		}
		
		dbContext.SaveChanges();

		return NoContent();
	}
}