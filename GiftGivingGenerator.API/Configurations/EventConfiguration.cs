using GiftGivingGenerator.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftGivingGenerator.API.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
	public void Configure(EntityTypeBuilder<Event> builder)
	{
		builder.HasMany(x => x.Persons)
			.WithMany(x => x.Events)
			.UsingEntity<Dictionary<string, object>>(
				"EventPerson",
				x => x
					.HasOne<Person>()
					.WithMany()
					.HasForeignKey("PersonId")
					.OnDelete(DeleteBehavior.Cascade),
				x => x
					.HasOne<Event>()
					.WithMany()
					.HasForeignKey("EventId")
					.OnDelete(DeleteBehavior.Restrict));

		builder.HasOne(x => x.Organizer)
			.WithMany(x => x.CreatedEvents)
			.HasForeignKey(x => x.OrganizerId);

		builder.HasMany(x => x.GiftWishes)
			.WithOne()
			.OnDelete(DeleteBehavior.NoAction);
	}
}