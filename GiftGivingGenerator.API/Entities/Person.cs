using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API.Entities;

public class Person : PersonBase
{
	public Guid OrganizerId { get; set; }
	public Organizer Organizer { get; set; }
	public bool? IsActive { get; private set; } = true;

	public void Deactivate()
	{
		IsActive = false;
	}

	//public List<GiftWish> GiftWishes { get; set; }
}