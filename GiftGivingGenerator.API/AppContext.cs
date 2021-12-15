using GiftGivingGenerator.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API;

internal class AppContext : DbContext
{
	public DbSet<Event> Events { get; set; }
	public DbSet<Person> Persons { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlServer("Server=localhost;Database=giftgiving;User Id=SA;Password=Your_password123;");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
	}
}