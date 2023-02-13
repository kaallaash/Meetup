using AutoMapper;
using FluentValidation;
using Meetup.API.ViewModels;
using Meetup.BLL.Interfaces;
using Meetup.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeakerController : Controller
{
    private readonly ISpeakerService<SpeakerModel, int> _speakerService;
    private readonly IMapper _mapper;
    private readonly IValidator<ChangeSpeakerViewModel> _changeSpeakerViewModelValidator;

    public SpeakerController(
        ISpeakerService<SpeakerModel, int> speakerService,
        IMapper mapper,
        IValidator<ChangeSpeakerViewModel> changeSpeakerViewModelValidator)
    {
        _speakerService = speakerService;
        _mapper = mapper;
        _changeSpeakerViewModelValidator = changeSpeakerViewModelValidator;
    }

    [HttpGet("{id}")]
    public async Task<SpeakerViewModel> Get(
        int id,
        CancellationToken cancellationToken)
    {
        var speaker = await _speakerService
            .GetById(id, cancellationToken);

        return _mapper.Map<SpeakerViewModel>(speaker);
    }

    [HttpGet]
    public async Task<IEnumerable<SpeakerViewModel>> GetAll(
        CancellationToken cancellationToken)
    {
        var speakers = await _speakerService
            .GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<SpeakerViewModel>>(speakers);
    }

    [HttpPost]
    public async Task<SpeakerViewModel> Create(
        [FromBody] ChangeSpeakerViewModel changeSpeakerViewModel,
        CancellationToken cancellationToken)
    {
        await _changeSpeakerViewModelValidator
            .ValidateAndThrowAsync(changeSpeakerViewModel, cancellationToken);

        var speakerModel = _mapper.Map<SpeakerModel>(changeSpeakerViewModel);

        var speaker = await _speakerService
            .Create(speakerModel, cancellationToken);

        return _mapper.Map<SpeakerViewModel>(speaker);
    }

    [HttpPut("{id}")]
    public async Task<SpeakerViewModel> Update(
        int id,
        [FromBody] ChangeSpeakerViewModel changeSpeakerViewModel,
      CancellationToken cancellationToken)
    {
        await _changeSpeakerViewModelValidator
            .ValidateAndThrowAsync(changeSpeakerViewModel, cancellationToken);

        var speakerModel = _mapper.Map<SpeakerModel>(changeSpeakerViewModel);
        speakerModel.Id = id;

        var result = await _speakerService
            .Update(speakerModel, cancellationToken);

        return _mapper.Map<SpeakerViewModel>(result);
    }

    [HttpDelete("{id}")]
    public Task Delete(
        int id,
        CancellationToken cancellationToken)
    {
        return _speakerService.Delete(id, cancellationToken);
    }
}