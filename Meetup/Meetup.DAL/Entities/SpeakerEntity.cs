using Meetup.Core.Entities;

namespace Meetup.DAL.Entities;

public class SpeakerEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
    public IEnumerable<EventEntity>? Events { get; set; }
}