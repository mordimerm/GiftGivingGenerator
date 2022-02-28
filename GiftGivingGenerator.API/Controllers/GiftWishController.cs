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
	
	[HttpPost("/Events/{eventId}/Persons/{personId}/GiftWishes")]
	public ActionResult UpdateGiftWish(Guid eventId, Guid personId, [FromBody] CreateGiftWishDto dto)
	{
		var giftWish = GiftWish.Create(eventId, personId, dto.Wish);
		_giftWishRepository.Remove(eventId, personId);
		_giftWishRepository.Insert(giftWish);
		
		return Ok();
	}

	[HttpGet("/Events/{eventId}/Persons/{personId}/GiftWishes")]
	public ActionResult GetByEventAndPerson(Guid eventId, Guid personId)
	{
		var giftWish = _giftWishRepository.GetByEventAndPerson(eventId, personId);
		
		return Ok(giftWish);
	}

	[HttpPut("{id}/Wish")]
	public ActionResult EditGiftWish(Guid id, [FromBody] EditGiftWishDto dto)
	{
		var giftWish = _giftWishRepository.Get(id);
		giftWish.EditWish(dto.Wish);
		_giftWishRepository.Update(giftWish);
		
		return Ok();
	}
}