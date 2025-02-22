using AdvicerApp.BL.DTOs.StatusDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.StatusValidators;

public class StatusCreateDtoValidator : AbstractValidator<StatusCreateDto>
{
    public StatusCreateDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty()
            .WithMessage("Content cannot be empty")
            .MaximumLength(512)
            .WithMessage("Content must be 512 characters");
    }
}
