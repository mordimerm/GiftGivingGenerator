using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IOrganizerRepository : IRepository<Organizer>
{
	List<PersonDto> GetOrganizers();

}