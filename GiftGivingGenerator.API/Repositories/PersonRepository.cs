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


	public List<PersonDto> GetPersonsByOrganizer(Guid organizerId)
	{
		var persons = DbContext.Persons
			.Where(x => x.OrganizerId == organizerId)
			.Where(x => x.IsActive == true)
			.ProjectTo<PersonDto>(Mapper.ConfigurationProvider)
			.ToList();
		return persons;
	}
	public List<Person> GetAllById(List<Guid> ids)
	{
		return DbContext.Persons.Where(x => ids.Contains(x.Id)).ToList();
	}
}