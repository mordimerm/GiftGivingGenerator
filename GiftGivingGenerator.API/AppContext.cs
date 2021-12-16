using GiftGivingGenerator.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API;

public class AppContext : DbContext
{
	public DbSet<Event> Events { get; set; }
	public DbSet<Person> Persons { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
	}
}