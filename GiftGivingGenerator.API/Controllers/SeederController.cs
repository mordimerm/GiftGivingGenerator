using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SeederController : ControllerBase
{
	private readonly ISeeder _seeder;
	public SeederController(ISeeder seeder)
	{
		_seeder = seeder;
	}

	[HttpDelete]
	public void RemoveAllDataInDb()
	{
		_seeder.RemoveAllDataInDb();
	}
	
	[HttpPost]
	public void Seed()
	{
		_seeder.Seed();
	}
}