namespace Meetup.BLL.Models;

public class SpeakerModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
    public IEnumerable<EventModel>? Events { get; set; }
}