using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Meetup.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meetup.DAL.IntegrationTests;

public class EventRepositoryIntegrationTests : IDisposable
{
    private readonly IEventRepository<EventEntity> _eventRepository;
    private readonly DatabaseContext _context;

    public EventRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_event_dal_db")
            .Options;

        _context = new DatabaseContext(options);
        _eventRepository = new EventRepository(_context);
    }

    public async void Dispose() => await _context.Database.EnsureDeletedAsync();
}