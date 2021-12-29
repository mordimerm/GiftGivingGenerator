using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class OrganizerRepository : RepositoryBase<Organizer>, IOrganizerRepository
{
	public OrganizerRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
	
	public List<PersonDto> GetOrganizers()
	{
		var persons = DbContext.Organizer
			.ProjectTo<PersonDto>(Mapper.ConfigurationProvider)
			.ToList();
		
		return persons;
	}
}