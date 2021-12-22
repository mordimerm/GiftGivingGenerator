using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IEventRepository : IRepository<Event>
{
	List<Event> GetEventsByOrganizerId(Guid organizerId);
}