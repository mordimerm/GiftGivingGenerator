namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventWithDrawingResultDto
{
	public string Name { get; set; }
	public Entities.Person Organizer { get; set; }
	public List<Entities.DrawingResult> DrawingResults { get; set; } = new List<Entities.DrawingResult>();
}