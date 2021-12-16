using GiftGivingGenerator.API.DataTransferObject;
using GiftGivingGenerator.API.DataTransferObject.Person;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	[HttpGet]
	public ActionResult<IEnumerable<PersonDto>> GetAllPersons()
	{
		var dbContext = new AppContext();
		var persons = dbContext
			.Persons
			.Select(x => new PersonDto
			{
				Id = x.Id,
				Name = x.Name,
			})
			.ToList();
		//TODO: may i do mapping with NuGet package AutoMapper.Extensions.Microsoft.DependencyInjection

		return Ok(persons);
	}
}