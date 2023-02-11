using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Meetup.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetup.DAL.DI;

public static class DataAccessRegister
{
    public static void AddDataContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEventRepository<EventEntity>, EventRepository>();
        services.AddScoped<ISpeakerRepository<SpeakerEntity>, SpeakerRepository>();

        services.AddDbContext<DatabaseContext>(op =>
        {
            op.UseSqlServer(
                configuration.GetConnectionString("MeetupDb"));
        });
    }
}