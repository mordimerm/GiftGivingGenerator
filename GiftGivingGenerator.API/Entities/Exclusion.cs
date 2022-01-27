namespace GiftGivingGenerator.API.Entities;

public class Exclusion : IEntity
{
	public Guid Id { get; set; }
	public Guid EventId { get; set; }
	public Event Event { get; set; }
	public Guid PersonId { get; set; }
	public Person Person { get; set; }
	public Guid ExcludedId { get; set; }
	public Person Excluded { get; set; }
}