using Meetup.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetup.BLL;

public static class BusinessLogicRegister
{
    public static void AddMeetupBll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //services.AddScoped<IEventService<Event, int>, EventService>();
        //services.AddScoped<ISpeakerService<Speaker, int>, SpeakerService>();
        services.AddDataContext(configuration);
    }
}