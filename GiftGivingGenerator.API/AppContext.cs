using GiftGivingGenerator.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API;

public class AppContext : DbContext
{
	public AppContext(DbContextOptions options): base(options)
	{
	}

	public DbSet<Event> Events { get; set; }
	public DbSet<Person> Persons { get; set; }
	
	public DbSet<Organizer> Organizer { get; set; }
	public DbSet<DrawingResult> DrawingResults { get; set; }
	public DbSet<GiftWish> GiftWish { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
	}
}