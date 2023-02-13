using AutoMapper;
using Meetup.BLL.Exceptions;
using Meetup.BLL.Services;
using Meetup.BLL.Tests.Helpers.ModelHelpers;
using Meetup.DAL.Entities;
using Meetup.DAL.Interfaces.Repositories;
using Moq;
using Xunit;
using static Meetup.BLL.Tests.Models.TestSpeakerModel;
using static Meetup.BLL.Tests.Entities.TestSpeakerEntity;
using Meetup.BLL.Models;
using Shouldly;

namespace Meetup.BLL.Tests;

public class SpeakerServiceTests
{
    private readonly SpeakerService _speakerService;
    private readonly Mock<ISpeakerRepository<SpeakerEntity>> _speakerRepository;
    private readonly Mock<IMapper> _mapper;

    public SpeakerServiceTests()
    {
        _speakerRepository = new Mock<ISpeakerRepository<SpeakerEntity>>();
        _mapper = new Mock<IMapper>();
        _speakerService = new SpeakerService(_speakerRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsSpeakerModel()
    {
        var validSpeakerEntity = GetValidSpeakerEntity;
        var validSpeakerModel = GetValidSpeakerModel;
        _speakerRepository
                .Setup(ur =>
                    ur.GetById(GetValidSpeakerModel.Id, default))
                .ReturnsAsync(validSpeakerEntity);
        _mapper
            .Setup(m => m.Map<SpeakerModel>(validSpeakerEntity))
            .Returns(validSpeakerModel);

        var result = await _speakerService.GetById(validSpeakerModel.Id, default);

        Assert.Equal(result?.Email, validSpeakerModel.Email);
        Assert.Equal(result?.Password, validSpeakerModel.Password);
    }

    [Fact]
    public async Task GetByEmail_ValidEmail_ReturnsSpeakerModel()
    {
        var validSpeakerModel = GetValidSpeakerModel;
        var validSpeakerEntity = GetValidSpeakerEntity;

        _speakerRepository
            .Setup(ur =>
                ur.GetByEmail(validSpeakerModel.Email, default))
            .ReturnsAsync(validSpeakerEntity);
        _mapper
            .Setup(m => m.Map<SpeakerModel>(validSpeakerEntity))
            .Returns(validSpeakerModel);

        var result = await _speakerService.GetByEmail(validSpeakerModel.Email, default);

        Assert.Equal(result.Name, validSpeakerModel.Name);
        Assert.Equal(result.Password, validSpeakerModel.Password);
    }

    [Fact]
    public async Task GetAll_ReturnsSpeakerModelList()
    {
        var validSpeakerEntities = GetValidSpeakerEntities;
        _speakerRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(validSpeakerEntities);
        _mapper
            .Setup(m => m.Map<IEnumerable<SpeakerModel>>(validSpeakerEntities))
            .Returns(GetValidSpeakerModels);

        var result = await _speakerService.GetAll(default);

        Assert.Equal(result.Count(), validSpeakerEntities.Count());
    }

    [Fact]
    public async Task Create_ValidSpeakerModel_ReturnsSpeakerModel()
    {
        var validSpeakerModel = GetValidSpeakerModel;
        var validSpeakerEntity = GetValidSpeakerEntity;

        _speakerRepository
            .Setup(ur =>
                ur.Create(validSpeakerEntity, default))
            .ReturnsAsync(validSpeakerEntity);
        _mapper
            .Setup(m => m.Map<SpeakerModel>(validSpeakerEntity))
            .Returns(GetValidSpeakerModel);
        _mapper
            .Setup(m => m.Map<SpeakerEntity>(validSpeakerModel))
            .Returns(validSpeakerEntity);

        var result = await _speakerService.Create(validSpeakerModel, default);

        Assert.Equal(result.Name, validSpeakerModel.Name);
        Assert.Equal(result.Email, validSpeakerModel.Email);
    }

    [Fact]
    public async Task Create_ExistedEmail_ThrowArgumentException()
    {
        _speakerRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(GetValidSpeakerEntities);

        await Should.ThrowAsync<ArgumentException>(
            async () => await _speakerService.Create(GetValidSpeakerModel, default));
    }

    [Fact]
    public async Task Update_ValidSpeakerModel_ReturnsSpeakerModel()
    {
        var validSpeakerModel = GetValidSpeakerModel;
        var validSpeakerEntity = GetValidSpeakerEntity;

        _speakerRepository
            .Setup(ur =>
                ur.Update(validSpeakerEntity, default))
            .ReturnsAsync(validSpeakerEntity);
        _speakerRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(GetValidSpeakerEntities);
        _speakerRepository
            .Setup(ur =>
                ur.GetById(validSpeakerEntity.Id, default))
            .ReturnsAsync(validSpeakerEntity);
        _mapper
            .Setup(m => m.Map<SpeakerModel>(validSpeakerEntity))
            .Returns(validSpeakerModel);
        _mapper
            .Setup(m => m.Map<SpeakerEntity>(validSpeakerModel))
            .Returns(validSpeakerEntity);

        var result = await _speakerService.Update(validSpeakerModel, default);

        Assert.Equal(result.Id, validSpeakerModel.Id);
        Assert.Equal(result.Email, validSpeakerModel.Email);
    }

    [Fact]
    public async Task Update_ExistedSpeakerEmail_ThrowArgumentException()
    {
        var validSpeakerModel = GetValidSpeakerModel;
        var validSpeakerEntity = GetValidSpeakerEntity;

        _speakerRepository
            .Setup(ur =>
                ur.Update(validSpeakerEntity, default))
            .ReturnsAsync(validSpeakerEntity);
        _speakerRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(GetValidSpeakerEntities);
        _speakerRepository
            .Setup(ur =>
                ur.GetById(validSpeakerEntity.Id, default))
            .ReturnsAsync(validSpeakerEntity);
        _mapper
            .Setup(m => m.Map<SpeakerModel>(validSpeakerEntity))
            .Returns(validSpeakerModel);
        _mapper
            .Setup(m => m.Map<SpeakerEntity>(validSpeakerModel))
            .Returns(validSpeakerEntity);

        validSpeakerModel.Email = SpeakerModelHelper.CreateValidSpeakerModel(2).Email;

        await Should.ThrowAsync<ArgumentException>(
            async () => await _speakerService.Update(validSpeakerModel, default));
    }

    [Fact]
    public async Task Delete_ValidId_NotThrowFoundException()
    {
        var validSpeakerEntity = GetValidSpeakerEntity;
        _speakerRepository
            .Setup(ur =>
                ur.GetById(validSpeakerEntity.Id, default))
            .ReturnsAsync(validSpeakerEntity);

        await Should.NotThrowAsync(
            async () => await _speakerService.Delete(validSpeakerEntity.Id, default));
    }

    [Fact]
    public async Task Delete_InValidId_ThrowFoundException()
    {
        var validSpeakerEntity = GetValidSpeakerEntity;
        _speakerRepository
            .Setup(ur =>
                ur.GetById(validSpeakerEntity.Id, default))
            .ReturnsAsync(validSpeakerEntity);

        await Should.ThrowAsync<NotFoundException>(
            async () => await _speakerService.Delete(validSpeakerEntity.Id + 1, default));
    }
}