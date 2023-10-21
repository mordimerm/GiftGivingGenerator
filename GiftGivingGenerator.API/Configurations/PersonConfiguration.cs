using GiftGivingGenerator.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftGivingGenerator.API.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder
			.HasMany<DrawingResult>()
			.WithOne(x => x.GiverPerson)
			.HasForeignKey(x => x.GiverPersonId)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.HasMany<DrawingResult>()
			.WithOne(x=>x.RecipientPerson)
			.HasForeignKey(x => x.RecipientPersonId)
			.OnDelete(DeleteBehavior.Restrict);
		
		builder
			.HasMany<GiftWish>()
			.WithOne(x => x.Person)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany<Exclusion>()
			.WithOne(x => x.Excluded)
			.HasForeignKey(x => x.ExcludedId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany<Exclusion>(x => x.Exclusions)
			.WithOne(x => x.Person)
			.HasForeignKey(x => x.PersonId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}