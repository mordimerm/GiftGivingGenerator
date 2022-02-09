using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IPersonRepository : IRepository<Person>
{
	List<Person> GetAllByIds(List<Guid> ids);
	
}