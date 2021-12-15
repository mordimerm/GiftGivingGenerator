using System.ComponentModel.DataAnnotations;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API;

public class Person
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public List<Event> Events { get; set; }
}