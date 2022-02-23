using AutoMapper;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
	public PersonRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
	
	public void InsertMany(List<Person> persons) 
	{ 
		DbContext.Persons.AddRange(persons); 
		DbContext.SaveChanges(); 
	} 
	
	public List<Person> GetAllByIds(List<Guid> ids)
	{
		return DbContext.Persons
			.Where(x => ids.Contains(x.Id))
			.ToList();
	}
	public void Delete(Guid id)
	{
		var person = DbContext.Persons
			.Single(x => x.Id == id);
		var exclusions = DbContext.Exclusions.
			Where(x => x.ExcludedId == id || x.PersonId == id);
		
		DbContext.Exclusions.RemoveRange(exclusions);
		DbContext.Persons.Remove(person);
		DbContext.SaveChanges();
	}
}