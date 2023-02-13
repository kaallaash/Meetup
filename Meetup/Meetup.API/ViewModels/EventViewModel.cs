namespace Meetup.API.ViewModels;

public class EventViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTimeOffset Date { get; set; }
    public SpeakerViewModel? Speaker { get; set; }
}