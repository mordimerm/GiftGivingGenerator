using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizerController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public OrganizerController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	[HttpPost]
	public ActionResult CreateOrganizer([FromBody] OrganizerDto organizerDto)
	{
		var organizer = _mapper.Map<OrganizerDto, Organizer>(organizerDto);
		_dbContext.Organizer.Add(organizer);
		
		var person = _mapper.Map<Organizer, Person>(organizer);

		_dbContext.Persons.Add(person);
		_dbContext.SaveChanges();

		return Created($"{organizer.Id}", null);
	}
}