namespace GiftGivingGenerator.API.Entities;

public class Organizer : PersonBase
{
	public string Email { get; set; }
	public string Password { get; set; }
	
	public List<Person> Persons { get; set; }
}