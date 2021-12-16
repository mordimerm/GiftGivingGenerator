using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Person;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly AppContext _dbContext;
	public PersonsController(IMapper mapper, AppContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}
	
	[HttpGet]
	public ActionResult<List<PersonDto>> GetAllPersons()
	{
		var personsDto = _dbContext
			.Persons
			.ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
			.ToList();
		
		return Ok(personsDto);
	}
}