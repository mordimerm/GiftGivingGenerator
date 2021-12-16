
using AutoMapper;
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
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public EventController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}
	[HttpPost]
	public ActionResult CreateEvent([FromBody] CreateEventDto get)
	{
		var even = new Event()
		{
			Name = get.Name,
			CreatingDate = DateTime.UtcNow,
			EndDate = get.EndDate,
		};

		_dbContext.Add(even);
		_dbContext.SaveChanges();

		return Created($"{even.Id}", null);
	}

	[HttpGet]
	public ActionResult GetOneEvent([FromBody] GetId get)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var even = _dbContext.Events
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
		var even = _dbContext.Events
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

		_dbContext.Update(even);
		_dbContext.SaveChanges();

		return Ok();
	}

	[HttpDelete]
	public ActionResult DeleteEvent([FromBody] GetId get)
	{
		var even = _dbContext.Events
			.Single(x => x.Id == get.Id);

		_dbContext.Remove(even);
		_dbContext.SaveChanges();

		return NoContent();
	}
}