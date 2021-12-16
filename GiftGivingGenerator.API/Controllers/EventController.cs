using GiftGivingGenerator.API.DataTransferObject;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Get;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
	[HttpPost]
	public ActionResult CreateEvent([FromBody] CreateEventDto get)
	{
		var even = new Event()
		{
			Name = get.Name,
			CreatingDate = DateTime.UtcNow,
			EndDate = get.EndDate,
		};

		var dbContext = new AppContext();
		dbContext.Add(even);
		dbContext.SaveChanges();

		return Created($"{even.Id}", null);
	}

	[HttpGet]
	public ActionResult GetOneEvent([FromBody] GetId get)
	{
		var dbContext = new AppContext();

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var even = dbContext.Events
			.Select(x => new EventDto()
			{
				Id = x.Id,
				Name = x.Name,
				EndDate = x.EndDate,
				Persons = x.Persons
					.Select(y => new PersonDto()
					{
						Id = y.Id,
						Name = y.Name
					})
			})
			.Single(x => x.Id == get.Id);

		return Ok(even);
	}

	[HttpPut]
	public ActionResult EditEvent([FromBody] EditEventDto get)
	{
		var dbContext = new AppContext();
		var even = dbContext.Events
			.Single(x => x.Id == get.Id);

		if (get.Name != "")
		{
			even.Name = get.Name;
		}
		
		//Paulina: ustalić, czy przekaże mi starą datę, czy ja mam to sprawdzać?
		if (get.EndDate != null)
		{
			even.EndDate = get.EndDate;
		}

		dbContext.Update(even);
		dbContext.SaveChanges();

		return Ok();
	}

	[HttpDelete]
	public ActionResult DeleteEvent([FromBody] GetId get)
	{
		var dbContext = new AppContext();
		var even = dbContext.Events
			.Single(x => x.Id == get.Id);

		dbContext.Remove(even);
		dbContext.SaveChanges();

		return NoContent();
	}
}