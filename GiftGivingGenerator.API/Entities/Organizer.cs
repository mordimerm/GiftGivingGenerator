namespace GiftGivingGenerator.API.Entities;

public class Organizer : PersonBase
{
	public string Email { get; set; }
	
	public List<Person> Persons { get; set; }
	
	public static Organizer Create(string name, string email)
	{
		var organizer = new Organizer()
		{
			Name = name,
			Email = email,
		};

		return organizer;
	}

	public Person AddPerson(string name)
	{
		var persons = Persons.SingleOrDefault(x => x.Name == name);
		
		if (persons!=null)
		{
			throw new Exception("There is person with the same name.");
		}
		
		var person = new Person()
		{
			Name = name,
			OrganizerId = Id,
		};
		
		Persons.Add(person);

		return person;
	}
}