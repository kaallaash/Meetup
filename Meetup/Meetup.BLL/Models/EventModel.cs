namespace Meetup.BLL.Models;

public class EventModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTimeOffset Date { get; set; }
    public int SpeakerId { get; set; }
    public SpeakerModel? Speaker { get; set; }
}