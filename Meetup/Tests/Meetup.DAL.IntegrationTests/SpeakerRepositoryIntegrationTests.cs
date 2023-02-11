using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Meetup.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

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
}