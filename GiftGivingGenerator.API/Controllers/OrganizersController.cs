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
	private readonly IPersonRepository _repositoryPerson;
	public OrganizersController(IOrganizerRepository repository, IPersonRepository repositoryPerson)
	{
		_repository = repository;
		_repositoryPerson = repositoryPerson;
	}

	[HttpPost]
	public ActionResult CreateOrganizer([FromBody] CreateOrganizerDto get)
	{
		var organizer = Organizer.Create(get.Name, get.Email, get.Password);
		var organizerId = _repository.Insert(organizer);
		
		return CreatedAtAction(nameof (CreateOrganizer), new {id = organizerId}, null);
	}

	[HttpPost ("/Authorization")]
	public ActionResult AuthorizeAndGetId([FromBody] OrganizerAuthorizationDto dto)
	{
		//Maciek: ask if it is correct?
		var authorization = new AuthorizationService(_repository); 
		var id = authorization.AuthorizateAndGetId(dto.Email, dto.Password);
		
		return Ok(id);
	}
	
	[HttpGet("{id}")]
	public ActionResult Get([FromRoute] Guid id)
	{
		var organizerDto = _repository.Get<OrganizerWithEventsDto>(id);
		
		return Ok(organizerDto);
	}
}