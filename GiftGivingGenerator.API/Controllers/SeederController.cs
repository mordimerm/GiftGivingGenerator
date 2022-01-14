using GiftGivingGenerator.API.HashingPassword;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SeederController : ControllerBase
{
	private readonly AppContext _dbContext;
	private readonly HashingOptions _options;
	private readonly IOrganizerRepository _organizerRepository;
	public SeederController(AppContext dbContext, HashingOptions options, IOrganizerRepository organizerRepository)
	{
		_dbContext = dbContext;
		_options = options;
		_organizerRepository = organizerRepository;
	}
	
	[HttpPost]
	public void Seed()
	{
		var seeder = new Seeder(_dbContext, _options, _organizerRepository);
		seeder.Seed();
	}
}