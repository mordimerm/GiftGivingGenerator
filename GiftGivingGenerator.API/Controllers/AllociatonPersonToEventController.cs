using GiftGivingGenerator.API.ModelsDataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AllociatonPersonToEventController : ControllerBase
{
	[HttpPut]
	public ActionResult AddPersonsToEvent([FromBody] GetEventIdAndListIdOfPersons get)
	{
		var dbContext = new AppContext();
		var even = dbContext.Events
			.Single(x => x.Id == get.EventId);

		even.Persons.Clear();

		foreach (var personId in get.PersonsId)
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
	public ActionResult RemovePersonsFromEvent([FromBody] GetEventIdAndListIdOfPersons get)
	{
		var dbContext = new AppContext();
		var even = dbContext.Events
			.Include(x=>x.Persons)
			.Single(x => x.Id == get.EventId);

		foreach (var personId in get.PersonsId)
		{
			var person = even.Persons
				.SingleOrDefault(x => x.Id == personId);
			even.Persons.Remove(person);
		}
		
		dbContext.SaveChanges();

		return NoContent();
	}
}