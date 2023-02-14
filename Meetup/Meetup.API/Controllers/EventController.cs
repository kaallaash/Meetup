using AutoMapper;
using FluentValidation;
using Meetup.API.ViewModels;
using Meetup.BLL.Interfaces;
using Meetup.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventController : Controller
{
    private readonly IEventService<EventModel, int> _eventService;
    private readonly IMapper _mapper;
    private readonly IValidator<ChangeEventViewModel> _changeEventViewModelValidator;

    public EventController(
        IEventService<EventModel, int> eventService,
        IMapper mapper,
        IValidator<ChangeEventViewModel> changeEventViewModelValidator)
    {
        _eventService = eventService;
        _mapper = mapper;
        _changeEventViewModelValidator = changeEventViewModelValidator;
    }

    [HttpGet("{id}")]
    public async Task<EventViewModel> Get(
        int id,
        CancellationToken cancellationToken)
    {
        var eventModel = await _eventService
            .GetById(id, cancellationToken);

        var result = _mapper.Map<EventViewModel>(eventModel);

        return _mapper.Map<EventViewModel>(eventModel);
    }

    [HttpGet]
    public async Task<IEnumerable<EventViewModel>> GetAll(
        CancellationToken cancellationToken)
    {
        var events = await _eventService
            .GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<EventViewModel>>(events);
    }

    [HttpPost]
    public async Task<EventViewModel> Create(
        [FromBody] ChangeEventViewModel changeEventViewModel,
        CancellationToken cancellationToken)
    {
        await _changeEventViewModelValidator
            .ValidateAndThrowAsync(changeEventViewModel, cancellationToken);

        var eventModel = _mapper.Map<EventModel>(changeEventViewModel);

        var eventViewModel = await _eventService
            .Create(eventModel, cancellationToken);

        return _mapper.Map<EventViewModel>(eventViewModel);
    }

    [HttpPut("{id}")]
    public async Task<EventViewModel> Update(
        int id,
        [FromBody] ChangeEventViewModel changeEventViewModel,
      CancellationToken cancellationToken)
    {
        await _changeEventViewModelValidator
            .ValidateAndThrowAsync(changeEventViewModel, cancellationToken);

        var eventModel = _mapper.Map<EventModel>(changeEventViewModel);
        eventModel.Id = id;

        var result = await _eventService
            .Update(eventModel, cancellationToken);

        return _mapper.Map<EventViewModel>(result);
    }

    [HttpDelete("{id}")]
    public Task Delete(
        int id,
        CancellationToken cancellationToken)
    {
        return _eventService.Delete(id, cancellationToken);
    }
}