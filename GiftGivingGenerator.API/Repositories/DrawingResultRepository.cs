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
	public List<DrawingResultDto> GetDrawingResultsByEventId(Guid id)
	{
		//TODO: we want give only givers names and links
		//there should be mapped only drawing result id without any additional data (especialy recipient name)
		return DbContext.DrawingResults
			.Where(x => x.EventId == id)
			.ProjectTo<DrawingResultDto>(Mapper.ConfigurationProvider)
			.ToList();
	}
}