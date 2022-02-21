using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class DrawingResultRepository : RepositoryBase<DrawingResult>, IDrawingResultRepository
{
	public DrawingResultRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
	public List<DrawingResultsForOrganizerDto> GetDrawingResultsByEventId(Guid id)
	{
		return DbContext.DrawingResults
			.Where(x => x.EventId == id)
			.ProjectTo<DrawingResultsForOrganizerDto>(Mapper.ConfigurationProvider)
			.ToList();
	}
	public DrawingResult? GetByPerson(Guid id)
	{
		return DbContext.DrawingResults
			.SingleOrDefault(x => x.GiverPersonId == id);
	}
}