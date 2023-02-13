using FluentValidation;
using Meetup.API.ViewModels;

namespace Meetup.API.Validators;

public class ChangeSpeakerViewModelValidator : AbstractValidator<ChangeSpeakerViewModel>
{
    public ChangeSpeakerViewModelValidator()
    {
        RuleFor(s => s.Name).NotEmpty().MinimumLength(2);
        RuleFor(s => s.Email).NotEmpty().EmailAddress();
        RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
    }
}