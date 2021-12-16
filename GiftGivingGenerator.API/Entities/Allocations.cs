namespace GiftGivingGenerator.API.Entities;

public class Allocations
{
	public Guid Id { get; set; }
	public Guid	EventId { get; set; }
	public Person DrawingPerson { get; set; }
	public Person SelectedPerson { get; set; }
	
	public virtual Event Events { get; set; }
	public virtual Person Persons { get; set; }
}