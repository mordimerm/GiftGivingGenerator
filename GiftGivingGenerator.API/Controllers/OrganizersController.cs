using GiftGivingGenerator.API.DataTransferObject.Organizer;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizersController : ControllerBase
{
	private readonly IOrganizerRepository _repository;
	public OrganizersController(IOrganizerRepository repository)
	{
		_repository = repository;
	}

	[HttpPost]
	public ActionResult CreateOrganizer([FromBody] CreateOrganizerDto get)
	{
		var organizer = Organizer.Create(get.Name, get.Email);
		var organizerId = _repository.Insert(organizer);
		
		return CreatedAtAction(nameof (CreateOrganizer), new {id = organizerId}, null);
	}
	
	[HttpGet("{id}")]
	public ActionResult Get([FromRoute] Guid id)
	{
		var organizerDto = _repository.Get<OrganizerWithEventsDto>(id);
		
		return Ok(organizerDto);
	}
}