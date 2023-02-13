namespace Meetup.API.ViewModels;

public class ChangeEventViewModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTimeOffset Date { get; set; }
    public int SpeakerId { get; set; }
}