using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.IntegrationTests.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Meetup.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static Meetup.DAL.IntegrationTests.Entities.TestEventEntity;

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

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task GetById_ValidId_ReturnsEventEntity(EventEntity eventEntity)
    {
        await AddAsync(_context, eventEntity);

        var createdEventEntity = await _context.Events
            .FirstOrDefaultAsync(e => e.Id == eventEntity.Id);

        createdEventEntity.ShouldNotBeNull();

        var actualEvent = await _eventRepository.GetById(createdEventEntity.Id, default);

        actualEvent.ShouldNotBeNull();
        actualEvent.Id.ShouldBe(createdEventEntity.Id);
    }

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task GetById_InvalidId_ReturnsNull(EventEntity eventEntity)
    {
        await AddAsync(_context, eventEntity);

        var actualEvent = await _eventRepository.GetById(eventEntity.Id + 1, default);
        actualEvent.ShouldBeNull();
    }
    
    [Fact]
    public async Task GetAll_ReturnsEventEntities()
    {
        await AddAsync(_context, GetValidEventEntitiesWithId());
        var eventsCount = _context.Events.Count();

        var actualEvents = await _eventRepository.GetAll(default);

        actualEvents.Count().ShouldBe(eventsCount);
    }

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task Create_ValidEventEntity_EntityIsCreated(
        EventEntity expectedValidEvent)
    {
        var actualEvent = await _eventRepository.Create(expectedValidEvent, default);

        actualEvent.ShouldNotBeNull();
        actualEvent.Title.ShouldBe(expectedValidEvent.Title);
        actualEvent.Description.ShouldBe(expectedValidEvent.Description);
    }

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task Update_ValidEventEntity_EntityIsUpdated(
        EventEntity eventEntity)
    {
        await AddAsync(_context, eventEntity);

        var updatedEventEntity = eventEntity;
        updatedEventEntity.Title += "+Title";

        var actualEvent = await _eventRepository.Update(updatedEventEntity, default);

        actualEvent.ShouldNotBeNull();
        actualEvent.Title.ShouldBe(updatedEventEntity.Title);
    }

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task Update_InvalidEventEntity_ThrowsException(
        EventEntity eventEntity)
    {
        await AddAsync(_context, eventEntity);

        var updatedEventEntity = eventEntity;
        updatedEventEntity.Id += 1;

        await Should.ThrowAsync<InvalidOperationException>
            (async () => await _eventRepository.Update(updatedEventEntity, default));
    }

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task Delete_ValidId_EntityIsDeleted(EventEntity eventEntity)
    {
        await AddAsync(_context, eventEntity);

        await Should.NotThrowAsync(
            async () => await _eventRepository.Delete(eventEntity, default));

        var deletedEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventEntity.Id);
        deletedEvent.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidEventEntities), MemberType = typeof(TestEventEntity))]
    public async Task Delete_InvalidId_ThrowsException(EventEntity eventEntity)
    {
        await Should.ThrowAsync<DbUpdateConcurrencyException>(
            async () => await _eventRepository.Delete(eventEntity, default));
    }

    private static async Task AddAsync(DatabaseContext context, EventEntity eventEntity)
    {
        await context.Events.AddAsync(eventEntity, default);
        await context.SaveChangesAsync();
    }

    private static async Task AddAsync(DatabaseContext context, IEnumerable<EventEntity> eventEntities)
    {
        await context.Events.AddRangeAsync(eventEntities, default);
        await context.SaveChangesAsync();
    }
}