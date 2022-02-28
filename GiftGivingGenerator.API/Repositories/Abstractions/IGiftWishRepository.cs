using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IGiftWishRepository : IRepository<GiftWish>
{
	public GiftWish GetByEventAndPerson(Guid eventId, Guid personId);
	
	void Remove(Guid eventId, Guid personId);
}