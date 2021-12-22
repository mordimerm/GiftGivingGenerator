using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.Entities;
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
	public DrawingResultsController(AppContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
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
	public ActionResult GetAllForEvent([FromRoute] Guid eventId)
	{
		var results = _dbContext.DrawingResults
			.Where(x => x.EventId == eventId)
			.ProjectTo<DrawingResultDto>(_mapper.ConfigurationProvider)
			.ToList();

		return Ok(results);
	}
	
	[HttpGet ("{id}")]
	public ActionResult Get([FromRoute] Guid id)
	{
		var drawingResult = _dbContext
			.DrawingResults
			.Single(x => x.Id == id);
	
		var drawingResultDto = _mapper.Map<DrawingResult, DrawingResultDto>(drawingResult);
		
		return Ok(drawingResultDto);
	}
}