using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IOrganizerRepository : IRepository<Organizer>
{
	public Organizer GetByEmail(string email);
}