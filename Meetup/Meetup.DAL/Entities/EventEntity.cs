using Meetup.Core.Entities;

namespace Meetup.DAL.Entities;

public class EventEntity : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public int SpeakerId { get; set; }
    public SpeakerEntity Speaker { get; set; }
}