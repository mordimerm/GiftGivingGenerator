namespace GiftGivingGenerator.API.Entities;

public class GiftWish : IEntity
{
	public Guid Id { get; set; }
	public string Wish { get; set; }

	public Guid EventId { get; set; }
	public Event Event { get; set; }

	public Guid PersonId { get; set; }
	public Person Person { get; set; }
}