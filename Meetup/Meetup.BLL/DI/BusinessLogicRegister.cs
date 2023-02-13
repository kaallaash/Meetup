using Meetup.BLL.Interfaces;
using Meetup.BLL.Models;
using Meetup.BLL.Services;
using Meetup.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetup.BLL.DI;

public static class BusinessLogicRegister
{
    public static void AddMeetupBll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEventService<EventModel, int>, EventService>();
        services.AddScoped<ISpeakerService<SpeakerModel, int>, SpeakerService>();
        services.AddDataContext(configuration);
    }
}