using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Repositories;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
	public EventRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}

	public List<EventToListDto> GetEventsByOrganizerId(Guid organizerId, bool? isActive, bool? isEndDateExpired)
	{
		var events = DbContext.Events
			.Where(x => x.OrganizerId == organizerId)
			.ProjectTo<EventToListDto>(Mapper.ConfigurationProvider)
			.ToList();

		switch (isActive)
		{
			case true:
				events = events.FindAll(x => x.IsActive);
				break;
			case false:
				events = events.FindAll(x => x.IsActive == false);
				break;
		}
		
		switch (isEndDateExpired)
		{
			case true:
				events = events.FindAll(x => x.EndDate < DateTime.UtcNow);
				break;
			case false:
				events = events.FindAll(x => x.EndDate >= DateTime.UtcNow);
				break;
		}
		
		return events;
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