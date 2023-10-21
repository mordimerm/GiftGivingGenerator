﻿namespace GiftGivingGenerator.API.Entities;

public class DrawingResult : IEntity
{
	public Guid Id { get; set; }
	public Guid	EventId { get; set; }
	public virtual Event Event { get; set; }
	public Guid GiverPersonId { get; set; }
	public virtual Person GiverPerson { get; set; }
	public Guid RecipientPersonId { get; set; }
	public virtual Person RecipientPerson { get; set; }
}