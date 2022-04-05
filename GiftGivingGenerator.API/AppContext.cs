using GiftGivingGenerator.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GiftGivingGenerator.API;

public class AppContext : DbContext
{
	public AppContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Event> Events { get; set; }
	public DbSet<Person> Persons { get; set; }
	public DbSet<DrawingResult> DrawingResults { get; set; }
	public DbSet<GiftWish> GiftWish { get; set; }
	public DbSet<Exclusion> Exclusions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
		
		var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
			v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			foreach (var property in entityType.GetProperties())
			{
				if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
					property.SetValueConverter(dateTimeConverter);
			}
		}
	}
}