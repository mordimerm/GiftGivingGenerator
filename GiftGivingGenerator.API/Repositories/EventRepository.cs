using AutoMapper;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
	public EventRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}

	public List<Event> GetEventsByOrganizerId(Guid organizerId)
	{
		return DbContext.Events.Where(x => x.OrganizerId == organizerId).ToList();
	}
}