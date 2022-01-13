using GiftGivingGenerator.API.DataTransferObject.Organizer;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.HashingPassword;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizersController : ControllerBase
{
	private readonly IOrganizerRepository _repository;
	private readonly HashingOptions _options;
	public OrganizersController(IOrganizerRepository repository, HashingOptions options)
	{
		_repository = repository;
		_options = options;
	}

	[HttpPost]
	public ActionResult CreateOrganizer([FromBody] CreateOrganizerDto get)
	{
		var hashedPassword = new PasswordHasher(_options).Hash(get.Password);
		var organizer = Organizer.Create(get.Name, get.Email, hashedPassword);
		var organizerId = _repository.Insert(organizer);
		
		return CreatedAtAction(nameof (CreateOrganizer), new {id = organizerId}, null);
	}

	[HttpPost ("/Authorization")]
	public ActionResult AuthorizeAndGetId([FromBody] OrganizerAuthorizationDto dto)
	{
		var authorization = new AuthorizationService(_repository, _options); 
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