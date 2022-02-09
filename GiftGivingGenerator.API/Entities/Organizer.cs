namespace GiftGivingGenerator.API.Entities;

public class Organizer : Person
{
	public string Email { get; set; }
	
	public List<Person> Persons { get; set; }
	
}