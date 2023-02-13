namespace Meetup.API.ViewModels;

public class SpeakerViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<EventViewModel>? Events { get; set; }
}