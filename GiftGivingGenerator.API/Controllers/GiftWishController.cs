using GiftGivingGenerator.API.DataTransferObject.GiftWish;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GiftWishController : ControllerBase
{
	private readonly IGiftWishRepository _giftWishRepository;

	public GiftWishController(IGiftWishRepository giftWishRepository)
	{
		_giftWishRepository = giftWishRepository;
	}
	
	[HttpPut("/Events/{eventId}/Persons/{personId}/GiftWishes")]
	public ActionResult CreateGiftWish(Guid eventId, Guid personId, [FromBody] CreateGiftWishDto dto)
	{
		var giftWish = _giftWishRepository.FindByEventAndPerson(eventId, personId);

		if (giftWish != null)
		{
			giftWish.Wish = dto.Wish;
			_giftWishRepository.Update(giftWish);
		}
		else
		{
			_giftWishRepository.Insert(GiftWish.Create(eventId, personId, dto.Wish));
		}
		
		return Ok();
	}
}