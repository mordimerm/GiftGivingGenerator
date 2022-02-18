using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

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
	public void Delete(Guid id)
	{
		var person = DbContext.Persons.Single(x => x.Id == id);
		// person.Events.Clear();
		//
		// var wish = DbContext.GiftWish.Single(x => x.PersonId == id);
		// DbContext.GiftWish.Remove(wish);
		//
		// var exclusions = DbContext.Exclusion.Where(x => x.ExcludedId == id || x.PersonId == id);
		// DbContext.Exclusion.RemoveRange(exclusions);
		
		DbContext.Persons.Remove(person);
		DbContext.SaveChanges();
	}
}