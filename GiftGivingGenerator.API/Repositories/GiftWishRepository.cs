using AutoMapper;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

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
}