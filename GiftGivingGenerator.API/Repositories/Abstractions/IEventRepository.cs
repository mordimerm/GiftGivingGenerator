using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IEventRepository : IRepository<Event>
{
	List<EventToListDto> GetByOrganizerId(Guid organizerId, bool? isActive, bool? isEndDateValid);
}