using AutoMapper;
using Meetup.BLL.Exceptions;
using Meetup.BLL.Models;
using Meetup.BLL.Services;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Moq;
using Shouldly;
using Xunit;
using static Meetup.BLL.Tests.Models.TestEventModel;
using static Meetup.BLL.Tests.Entities.TestEventEntity;

namespace Meetup.BLL.Tests;

public class EventServiceTests
{
    private readonly EventService _eventService;
    private readonly Mock<IEventRepository<EventEntity>> _eventRepository;
    private readonly Mock<IMapper> _mapper;

    public EventServiceTests()
    {
        _eventRepository = new Mock<IEventRepository<EventEntity>>();
        _mapper = new Mock<IMapper>();
        _eventService = new EventService(_eventRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsEventModel()
    {
        var validEventEntity = GetValidEventEntity;
        var validEventModel = GetValidEventModel;
        _eventRepository
                .Setup(ur =>
                    ur.GetById(GetValidEventModel.Id, default))
                .ReturnsAsync(validEventEntity);
        _mapper
            .Setup(m => m.Map<EventModel>(validEventEntity))
            .Returns(validEventModel);

        var result = await _eventService.GetById(validEventModel.Id, default);

        Assert.Equal(result?.Title, validEventModel.Title);
        Assert.Equal(result?.Description, validEventModel.Description);
    }

    [Fact]
    public async Task GetAll_ReturnsEventModelList()
    {
        var validEventEntities = GetValidEventEntities;
        _eventRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(validEventEntities);
        _mapper
            .Setup(m => m.Map<IEnumerable<EventModel>>(validEventEntities))
            .Returns(GetValidEventModels);

        var result = await _eventService.GetAll(default);

        Assert.Equal(result.Count(), validEventEntities.Count());
    }

    [Fact]
    public async Task Create_ValidEventModel_ReturnsEventModel()
    {
        var validEventModel = GetValidEventModel;
        var validEventEntity = GetValidEventEntity;

        _eventRepository
            .Setup(ur =>
                ur.Create(validEventEntity, default))
            .ReturnsAsync(validEventEntity);
        _mapper
            .Setup(m => m.Map<EventModel>(validEventEntity))
            .Returns(GetValidEventModel);
        _mapper
            .Setup(m => m.Map<EventEntity>(validEventModel))
            .Returns(validEventEntity);

        var result = await _eventService.Create(validEventModel, default);

        Assert.Equal(result.Title, validEventModel.Title);
        Assert.Equal(result.Description, validEventModel.Description);
    }

    [Fact]
    public async Task Update_ValidEventModel_ReturnsEventModel()
    {
        var validEventModel = GetValidEventModel;
        var validEventEntity = GetValidEventEntity;

        _eventRepository
            .Setup(ur =>
                ur.Update(validEventEntity, default))
            .ReturnsAsync(validEventEntity);
        _eventRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(GetValidEventEntities);
        _eventRepository
            .Setup(ur =>
                ur.GetById(validEventEntity.Id, default))
            .ReturnsAsync(validEventEntity);
        _mapper
            .Setup(m => m.Map<EventModel>(validEventEntity))
            .Returns(validEventModel);
        _mapper
            .Setup(m => m.Map<EventEntity>(validEventModel))
            .Returns(validEventEntity);

        var result = await _eventService.Update(validEventModel, default);

        Assert.Equal(result.Id, validEventModel.Id);
        Assert.Equal(result.Title, validEventModel.Title);
    }

    [Fact]
    public async Task Delete_ValidId_NotThrowFoundException()
    {
        var validEventEntity = GetValidEventEntity;
        _eventRepository
            .Setup(ur =>
                ur.GetById(validEventEntity.Id, default))
            .ReturnsAsync(validEventEntity);

        await Should.NotThrowAsync(
            async () => await _eventService.Delete(validEventEntity.Id, default));
    }

    [Fact]
    public async Task Delete_InValidId_ThrowFoundException()
    {
        var validEventEntity = GetValidEventEntity;
        _eventRepository
            .Setup(ur =>
                ur.GetById(validEventEntity.Id, default))
            .ReturnsAsync(validEventEntity);

        await Should.ThrowAsync<NotFoundException>(
            async () => await _eventService.Delete(validEventEntity.Id + 1, default));
    }
}