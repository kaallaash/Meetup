using Meetup.DAL.Context;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meetup.DAL.Repositories;

public class EventRepository : IEventRepository<EventEntity>
{
    private readonly DatabaseContext _db;

    public EventRepository(DatabaseContext db)
    {
        _db = db;
    }
    public async Task<EventEntity> Create(EventEntity eventEntity, CancellationToken cancellationToken)
    {
        var drawingEntity = await _db.Events.AddAsync(eventEntity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity.Entity;
    }

    public async Task Delete(EventEntity eventEntity, CancellationToken cancellationToken)
    {
        _db.Events.Remove(eventEntity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<EventEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _db.Events
            .AsNoTracking()
            .Include(e => e.Speaker)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<EventEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Events
            .AsNoTracking()
            .Include(e => e.Speaker)
            .ToListAsync(cancellationToken);
    }

    public async Task<EventEntity> Update(EventEntity eventEntity, CancellationToken cancellationToken)
    {
        _db.Entry(eventEntity).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);

        return eventEntity;
    }
}