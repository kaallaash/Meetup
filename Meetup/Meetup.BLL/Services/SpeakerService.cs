using AutoMapper;
using Meetup.BLL.Exceptions;
using Meetup.BLL.Interfaces;
using Meetup.BLL.Models;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;

namespace Meetup.BLL.Services;

public class SpeakerService : ISpeakerService<SpeakerModel, int>
{
    private readonly ISpeakerRepository<SpeakerEntity> _speakerRepository;
    private readonly IMapper _mapper;

    public SpeakerService(
        ISpeakerRepository<SpeakerEntity> speakerRepository,
        IMapper mapper)
    {
        _speakerRepository = speakerRepository;
        _mapper = mapper;
    }

    public async Task<SpeakerModel> Create(SpeakerModel speaker, CancellationToken cancellationToken)
    {
        var speakerEntities = await _speakerRepository.GetAll(cancellationToken);

        if (speakerEntities.Any(s =>
                s?.Email == speaker?.Email))
        {
            throw new ArgumentException("email already exists");
        }

        var speakerEntity = _mapper.Map<SpeakerEntity>(speaker);
        var createdSpeaker = await _speakerRepository.Create(speakerEntity, cancellationToken);

        return _mapper.Map<SpeakerModel>(createdSpeaker);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var speakerEntity = await _speakerRepository.GetById(id, cancellationToken);

        if (speakerEntity is null)
        {
            throw new NotFoundException("The speaker is not found");
        }

        await _speakerRepository.Delete(speakerEntity, cancellationToken);
    }

    public async Task<SpeakerModel?> GetById(int id, CancellationToken cancellationToken)
    {
        var speakerEntity = await _speakerRepository.GetById(id, cancellationToken);

        return _mapper.Map<SpeakerModel>(speakerEntity);
    }

    public async Task<SpeakerModel?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var speakerEntity = await _speakerRepository.GetByEmail(email, cancellationToken);

        return _mapper.Map<SpeakerModel?>(speakerEntity);
    }

    public async Task<IEnumerable<SpeakerModel>> GetAll(CancellationToken cancellationToken)
    {
        var speakerEntities = await _speakerRepository.GetAll(cancellationToken);

        return _mapper.Map<List<SpeakerModel>>(speakerEntities);
    }

    public async Task<SpeakerModel> Update(SpeakerModel speaker, CancellationToken cancellationToken)
    {
        var speakerEntity = await _speakerRepository.GetById(speaker.Id, cancellationToken);

        if (speakerEntity is null)
        {
            throw new NotFoundException("The speaker is not found");
        }

        var speakerEntities = await _speakerRepository.GetAll(cancellationToken);

        var result = speakerEntities.Any(s =>
            s?.Email == speaker?.Email
            && s?.Id != speaker?.Id);

        if (result)
        {
            throw new ArgumentException("The same email already exists");
        }

        var speakerAfterMap = _mapper.Map<SpeakerEntity>(speaker);
        await _speakerRepository.Update(speakerAfterMap, cancellationToken);

        return speaker;
    }
}