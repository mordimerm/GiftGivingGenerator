using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizerController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	private readonly IOrganizerRepository _repository;
	public OrganizerController(IMapper mapper, AppContext dbContext, IOrganizerRepository repository)
	{
		_mapper = mapper;
		_dbContext = dbContext;
		_repository = repository;
	}

	[HttpPost]
	public ActionResult CreateOrganizer([FromBody] OrganizerDto organizerDto)
	{
		var organizer = _mapper.Map<OrganizerDto, Organizer>(organizerDto);
		_dbContext.Organizer.Add(organizer);
		
		var person = _mapper.Map<Organizer, Person>(organizer);
		_dbContext.Persons.Add(person);
		
		_dbContext.SaveChanges();

		return CreatedAtAction(nameof (CreateOrganizer), new {id = organizer.Id}, organizer);
	}
}