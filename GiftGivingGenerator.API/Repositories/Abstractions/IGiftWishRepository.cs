using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IGiftWishRepository : IRepository<GiftWish>
{
	public GiftWish? FindByEventAndPerson(Guid eventId, Guid personId);
}