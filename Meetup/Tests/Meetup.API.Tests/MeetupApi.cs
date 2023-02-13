using Meetup.DAL.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Meetup.Core.Tests.FakePolicy;

namespace Meetup.API.Tests;

internal class MeetupApi : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DatabaseContext>));
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase("TestDb", root));
        });

        return base.CreateHost(builder);
    }
}