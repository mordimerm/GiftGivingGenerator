using GiftGivingGenerator.API.DataTransferObject.GiftWish;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GiftWishController : ControllerBase
{
	private readonly IGiftWishRepository _repository;

	public GiftWishController(IGiftWishRepository repository)
	{
		_repository = repository;
	}
	
	[HttpPost("/Events/{eventId}/Persons/{personId}/GiftWishes")]
	public ActionResult CreateGiftWish(Guid eventId, Guid personId, [FromBody] CreateGiftWishDto dto)
	{
		var giftWish = GiftWish.Create(eventId, personId, dto.Wish);
		_repository.Insert(giftWish);
		
		return Ok();
	}

	[HttpGet("/Events/{eventId}/Persons/{personId}/GiftWishes")]
	public ActionResult GetByEventAndPerson(Guid eventId, Guid personId)
	{
		var giftWish = _repository.GetByEventAndPerson(eventId, personId);
		
		return Ok(giftWish);
	}

	[HttpPut("{id}/Wish")]
	public ActionResult EditGiftWish(Guid id, [FromBody] EditGiftWishDto dto)
	{
		var giftWish = _repository.Get(id);
		giftWish.EditWish(dto.Wish);
		_repository.Update(giftWish);
		
		return Ok();
	}
}