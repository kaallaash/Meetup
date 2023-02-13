namespace Meetup.API.Validators;
using FluentValidation;
using Meetup.API.ViewModels;

public class ChangeEventViewModelValidator : AbstractValidator<ChangeEventViewModel>
{
    public ChangeEventViewModelValidator()
    {
        RuleFor(e => e.Title).NotEmpty().MinimumLength(2);
        RuleFor(e => e.Description).NotEmpty().MinimumLength(10);
        RuleFor(e => e.Location).NotEmpty();
        RuleFor(e => e.Date).GreaterThan(DateTimeOffset.Now);
    }
}