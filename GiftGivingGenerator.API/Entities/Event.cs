namespace GiftGivingGenerator.API.Entities;

public class Event
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatingDate { get; set; }
	//TODO: EndData
	public List<Person> Persons { get; set; }
}