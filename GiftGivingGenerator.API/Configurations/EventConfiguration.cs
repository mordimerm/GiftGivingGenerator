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
					.HasForeignKey("PersonId"),
				x => x
					.HasOne<Event>()
					.WithMany()
					.HasForeignKey("EventId"));
	}
}