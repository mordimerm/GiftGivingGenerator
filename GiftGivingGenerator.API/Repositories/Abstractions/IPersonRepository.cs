using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IPersonRepository : IRepository<Person>
{
	List<Person> GetAllByIds(List<Guid> ids);
	void InsertMany(List<Person> persons);
}