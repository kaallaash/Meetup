using Meetup.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static Meetup.API.Tests.Entities.TestSpeakerEntity;
using static Meetup.API.Tests.Entities.TestEventEntity;

namespace Meetup.API.Tests.Helpers;

internal static class ClientBuilder
{
    internal static async Task<HttpClient> CreateSpeakerClient(MeetupApi application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using var dbContext = provider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Speakers.AddRangeAsync(GetValidCreatedSpeakerEntities());
            await dbContext.SaveChangesAsync();
        }

        return application.CreateClient();
    }

    internal static async Task<HttpClient> CreateEventClient(MeetupApi application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using var dbContext = provider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Speakers.AddRangeAsync(GetValidCreatedSpeakerEntities());
            await dbContext.Events.AddRangeAsync(GetValidCreatedEventEntities());
            await dbContext.SaveChangesAsync();
        }

        return application.CreateClient();
    }
}