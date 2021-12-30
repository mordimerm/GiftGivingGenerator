namespace GiftGivingGenerator.API.Entities;

public class Person : PersonBase
{
	public Guid OrganizerId { get; set; }
	public Organizer Organizer { get; set; }
	public bool? IsActive { get; private set; } = true;

	public static Person Create(string name, Guid id)
	{
		var person = new Person()
		{
			Name = name,
			OrganizerId = id,
		};

		return person;
	}
	
	public void Deactivate()
	{
		IsActive = false;
	}
}