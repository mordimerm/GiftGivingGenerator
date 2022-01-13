using GiftGivingGenerator.API.HashingPassword;
using Microsoft.Extensions.Options;

namespace GiftGivingGenerator.API.Entities;

public class Organizer : PersonBase
{
	private static IOptions<HashingOptions> _options;
	public Organizer(IOptions<HashingOptions> options)
	{
		_options = options;
	}
	public string Email { get; set; }
	public string Password { get; set; }
	
	public List<Person> Persons { get; set; }
	
	public static Organizer Create(string name, string email, string password)
	{
		var passwordHasher = new PasswordHasher(_options);
		var organizer = new Organizer(_options)
		{
			Name = name,
			Email = email,
			Password = passwordHasher.Hash(password),
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