using Meetup.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static AuthorizationService.API.Tests.Entities.TestSpeakerEntity;

namespace AuthorizationService.API.Tests.Helpers;

internal static class ClientBuilder
{
    internal static async Task<HttpClient> CreateClient(AuthorizationApi application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using var dbContext = provider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Speakers.AddRangeAsync(GetValidSpeakerEntities);
            await dbContext.SaveChangesAsync();
        }

        return application.CreateClient();
    }
}