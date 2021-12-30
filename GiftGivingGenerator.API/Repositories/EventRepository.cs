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

	public List<EventToListDto> GetEventsByOrganizerId(Guid organizerId)
	{
		var events = DbContext.Events
			.Where(x => x.OrganizerId == organizerId)
			.ProjectTo<EventToListDto>(Mapper.ConfigurationProvider)
			.ToList();
		
		return events;
	}

	public override IQueryable<Event> WriteEntitySet()
	{
		return base.WriteEntitySet()
			.Include(x=>x.Persons)
			.Include(x=>x.DrawingResults);
	}
}