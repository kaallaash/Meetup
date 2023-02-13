using AutoMapper;
using Meetup.BLL.Exceptions;
using Meetup.BLL.Interfaces;
using Meetup.BLL.Models;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;

namespace Meetup.BLL.Services;

public class EventService : IEventService<EventModel, int>
{
    private readonly IEventRepository<EventEntity> _eventRepository;
    private readonly IMapper _mapper;

    public EventService(
        IEventRepository<EventEntity> eventRepository,
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventModel> Create(EventModel eventModel, CancellationToken cancellationToken)
    {
        var eventEntity = _mapper.Map<EventEntity>(eventModel);
        var createdEvent = await _eventRepository.Create(eventEntity, cancellationToken);

        return _mapper.Map<EventModel>(createdEvent);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetById(id, cancellationToken);

        if (eventEntity is null)
        {
            throw new NotFoundException("The event is not found");
        }

        await _eventRepository.Delete(eventEntity, cancellationToken);
    }

    public async Task<EventModel?> GetById(int id, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetById(id, cancellationToken);

        return _mapper.Map<EventModel>(eventEntity);
    }

    public async Task<IEnumerable<EventModel>> GetAll(CancellationToken cancellationToken)
    {
        var eventEntities = await _eventRepository.GetAll(cancellationToken);

        return _mapper.Map<List<EventModel>>(eventEntities);
    }

    public async Task<EventModel> Update(EventModel eventModel, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetById(eventModel.Id, cancellationToken);

        if (eventEntity is null)
        {
            throw new NotFoundException("The event is not found");
        }

        var eventAfterMap = _mapper.Map<EventEntity>(eventModel);
        await _eventRepository.Update(eventAfterMap, cancellationToken);

        return eventModel;
    }
}
