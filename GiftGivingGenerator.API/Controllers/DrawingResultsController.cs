using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DrawingResultsController : ControllerBase
{
	private readonly AppContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IDrawingResultRepository _repository;
	public DrawingResultsController(AppContext dbContext, IMapper mapper, IDrawingResultRepository repository)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_repository = repository;
	}
	
	[HttpPost("/{eventId}/[controller]")]
	public ActionResult GenerateDrawingResults([FromRoute] Guid eventId)
	{
		var @event = _dbContext.Events
			.Include(x => x.Persons)
			.Include(x => x.DrawingResults)
			.Single(x => x.Id == eventId);

		if (@event.DrawingResults.Count != 0)
		{
			ModelState.AddModelError(nameof(GenerateDrawingResults), "The draw was genereted. Check it with httpget.");
			return BadRequest(ModelState);
		}

		var personsIds = @event.Persons
			.Select(x => x.Id)
			.ToList();

		var permutationA = MoreEnumerable.Shuffle(personsIds).ToList();
		var permutationB = new List<Guid>();

		var i = 0;
		do
		{
			permutationB = MoreEnumerable.Shuffle(personsIds).ToList();
			for (i = 0; i < permutationA.Count; i++)
			{
				if (permutationA[i] == permutationB[i])
				{
					break;
				}
			}
		}
		while (personsIds.Count != i);

		for (int j = 0; j < personsIds.Count; j++)
		{
			var drawingResult = new DrawingResult
			{
				EventId = eventId,
				GiverPersonId = permutationA[j],
				RecipientPersonId = permutationB[j],
			};

			_dbContext.DrawingResults.Add(drawingResult);
		}

		_dbContext.SaveChanges();

		return Ok();
	}

	[HttpGet("/{eventId}/[controller]")]
	public ActionResult<List<DrawingResultDto>> GetAllForEvent([FromRoute] Guid eventId)
	{
		return Ok(_repository.GetDrawingResultsByEventId(eventId));
	}
	
	[HttpGet ("{id}")]
	public ActionResult<DrawingResultDto> Get([FromRoute] Guid id)
	{
		return Ok(_repository.Get<DrawingResultDto>(id));
	}
}