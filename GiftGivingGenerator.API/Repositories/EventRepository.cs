using AutoMapper;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Repositories;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
	public EventRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
	
	public override IQueryable<Event> WriteEntitySet()
	{
		return base.WriteEntitySet()
			.Include(x=>x.Persons)
			.Include(x=>x.DrawingResults)
			.Include(x=>x.GiftWishes)
			.Include(x=>x.Exclusions)
			.AsSplitQuery();
	}
}