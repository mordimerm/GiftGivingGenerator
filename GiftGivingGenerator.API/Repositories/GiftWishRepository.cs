using AutoMapper;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class GiftWishRepository : RepositoryBase<GiftWish>, IGiftWishRepository
{
	public GiftWishRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}

	public GiftWish GetByEventAndPerson(Guid eventId, Guid personsId)
	{
		var giftWish = DbContext.GiftWish
			.Single(x => x.EventId == eventId && x.PersonId == personsId);
		
		return giftWish;
	}
	public void Remove(Guid eventId, Guid personId)
	{
		var giftWish = DbContext.GiftWish.SingleOrDefault(x => x.EventId == eventId && x.PersonId == personId);
		if (giftWish!=null)
		{
			DbContext.GiftWish.Remove(giftWish);
			DbContext.SaveChanges();
		}
	}
}