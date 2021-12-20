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
			.WithOne()
			.HasForeignKey(x => x.GiverPersonId)
			.OnDelete(DeleteBehavior.Restrict);
		
		builder
			.HasMany<DrawingResult>()
			.WithOne()
			.HasForeignKey(x => x.RecipientPersonId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}