using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meetup.DAL.Repositories;

public class SpeakerRepository : ISpeakerRepository<SpeakerEntity>
{
    private readonly DatabaseContext _db;

    public SpeakerRepository(DatabaseContext db)
    {
        _db = db;
    }
    public async Task<SpeakerEntity> Create(SpeakerEntity speaker, CancellationToken cancellationToken)
    {
        var drawingEntity = await _db.Speakers.AddAsync(speaker, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity.Entity;
    }

    public async Task Delete(SpeakerEntity speaker, CancellationToken cancellationToken)
    {
        _db.Speakers.Remove(speaker);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<SpeakerEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _db.Speakers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<SpeakerEntity?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _db.Speakers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<SpeakerEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Speakers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<SpeakerEntity> Update(SpeakerEntity speaker, CancellationToken cancellationToken)
    {
        _db.Entry(speaker).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);

        return speaker;
    }
}