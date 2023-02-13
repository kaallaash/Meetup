using Meetup.BLL.Models;
using Meetup.BLL.Tests.Helpers.ModelHelpers;

namespace Meetup.BLL.Tests.Models;

public static class TestSpeakerModel
{
    public static SpeakerModel GetValidSpeakerModel => SpeakerModelHelper.CreateValidSpeakerModel(1);

    public static IEnumerable<SpeakerModel> GetValidSpeakerModels => new List<SpeakerModel>()
    {
        SpeakerModelHelper.CreateValidSpeakerModel(1),
        SpeakerModelHelper.CreateValidSpeakerModel(2),
        SpeakerModelHelper.CreateValidSpeakerModel(3),
        SpeakerModelHelper.CreateValidSpeakerModel(4),
        SpeakerModelHelper.CreateValidSpeakerModel(5)
    };
}