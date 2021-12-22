using AutoMapper;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public class OrganizerRepository : RepositoryBase<Organizer>, IOrganizerRepository
{
	public OrganizerRepository(AppContext dbContext, IMapper mapper) : base(dbContext, mapper)
	{
	}
}