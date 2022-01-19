using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IEventRepository : IRepository<Event>
{
	List<EventToListDto> GetEventsByOrganizerId(Guid organizerId, bool? isActive, bool? isEndDateValid);
	
}