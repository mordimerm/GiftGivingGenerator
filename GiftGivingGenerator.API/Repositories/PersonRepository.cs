using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
	public PersonRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
	
	public List<Person> GetAllByIds(List<Guid> ids)
	{
		return DbContext.Persons
			.Where(x => ids.Contains(x.Id))
			.ToList();
	}
	public void Delete(Person person)
	{
		DbContext.Persons.Remove(person);
		DbContext.SaveChanges();
	}
}