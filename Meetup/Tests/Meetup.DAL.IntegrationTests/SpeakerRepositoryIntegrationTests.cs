using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.IntegrationTests.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Meetup.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static Meetup.DAL.IntegrationTests.Entities.TestSpeakerEntity;

namespace Meetup.DAL.IntegrationTests;

public class SpeakerRepositoryIntegrationTests : IDisposable
{
    private readonly ISpeakerRepository<SpeakerEntity> _speakerRepository;
    private readonly DatabaseContext _context;

    public SpeakerRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_speaker_dal_db")
            .Options;

        _context = new DatabaseContext(options);
        _speakerRepository = new SpeakerRepository(_context);
    }
    public async void Dispose() => await _context.Database.EnsureDeletedAsync();

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task GetById_ValidId_ReturnsSpeakerEntity(SpeakerEntity speaker)
    {
        await AddAsync(_context, speaker);

        var createdSpeakerEntity = await _context.Speakers
            .FirstOrDefaultAsync(s => s.Email == speaker.Email
                                      && s.Password == speaker.Password);

        createdSpeakerEntity.ShouldNotBeNull();

        var actualSpeaker = await _speakerRepository.GetById(createdSpeakerEntity.Id, default);

        actualSpeaker.ShouldNotBeNull();
        actualSpeaker.Id.ShouldBe(createdSpeakerEntity.Id);
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task GetById_InvalidId_ReturnsNull(SpeakerEntity speaker)
    {
        await AddAsync(_context, speaker);

        var actualSpeaker = await _speakerRepository.GetById(speaker.Id + 1, default);
        actualSpeaker.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task GetByEmail_ValidEmail_ReturnsSpeakerEntity(SpeakerEntity speaker)
    {
        await AddAsync(_context, speaker);

        var createdSpeakerEntity = await _context.Speakers
            .FirstOrDefaultAsync(s => s.Email == speaker.Email
                                      && s.Password == speaker.Password);

        createdSpeakerEntity.ShouldNotBeNull();
        
        var actualSpeaker = await _speakerRepository.GetByEmail(createdSpeakerEntity.Email, default);

        actualSpeaker.ShouldNotBeNull();
        actualSpeaker.Id.ShouldBe(createdSpeakerEntity.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsSpeakerEntities()
    {
        await AddAsync(_context, GetValidSpeakerEntitiesWithId());
        var speakersCount = _context.Speakers.Count();

        var actualSpeakers = await _speakerRepository.GetAll(default);
        actualSpeakers.Count().ShouldBe(speakersCount);
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task Create_ValidSpeakerEntity_EntityIsCreated(
        SpeakerEntity expectedValidSpeaker)
    {
        var actualSpeaker = await _speakerRepository.Create(expectedValidSpeaker, default);

        actualSpeaker.ShouldNotBeNull();
        actualSpeaker.Email.ShouldBe(expectedValidSpeaker.Email);
        actualSpeaker.Password.ShouldBe(expectedValidSpeaker.Password);
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task Update_ValidSpeakerEntity_EntityIsUpdated(
        SpeakerEntity speaker)
    {
        await AddAsync(_context, speaker);

        var updatedSpeakerEntity = speaker;
        updatedSpeakerEntity.Email += ".com";

        var actualSpeaker = await _speakerRepository.Update(updatedSpeakerEntity, default);

        actualSpeaker.ShouldNotBeNull();
        actualSpeaker.Email.ShouldBe(updatedSpeakerEntity.Email);
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task Update_InvalidSpeakerEntity_ThrowsException(
        SpeakerEntity speaker)
    {
        await AddAsync(_context, speaker);

        var updatedSpeakerEntity = speaker;
        updatedSpeakerEntity.Id += 1;

        await Should.ThrowAsync<InvalidOperationException>
            (async () => await _speakerRepository.Update(updatedSpeakerEntity, default));
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task Delete_ValidId_EntityIsDeleted(SpeakerEntity speaker)
    {
        await AddAsync(_context, speaker);

        await Should.NotThrowAsync(
            async () => await _speakerRepository.Delete(speaker, default));

        var deletedSpeaker = await _context.Speakers.FirstOrDefaultAsync(s => s.Id == speaker.Id);
        deletedSpeaker.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidSpeakerEntities), MemberType = typeof(TestSpeakerEntity))]
    public async Task Delete_InvalidId_ThrowsException(SpeakerEntity speaker)
    {
        await Should.ThrowAsync<DbUpdateConcurrencyException>(
            async () => await _speakerRepository.Delete(speaker, default));
    }

    private static async Task AddAsync(DatabaseContext context, SpeakerEntity speakerEntity)
    {
        await context.Speakers.AddAsync(speakerEntity, default);
        await context.SaveChangesAsync();
    }

    private static async Task AddAsync(DatabaseContext context, IEnumerable<SpeakerEntity> speakerEntities)
    {
        await context.Speakers.AddRangeAsync(speakerEntities, default);
        await context.SaveChangesAsync();
    }
}