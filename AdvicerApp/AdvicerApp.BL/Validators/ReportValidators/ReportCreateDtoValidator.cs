using AdvicerApp.BL.DTOs.ReportDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.ReportValidators;

public class ReportCreateDtoValidator : AbstractValidator<ReportCreateDto>
{
    public ReportCreateDtoValidator()
    {
        RuleFor(x => x.Reason)
            .NotNull()
            .NotEmpty()
            .WithMessage("Reason cannot be empty")
            .MaximumLength(256)
            .WithMessage("Reason can maximum 256 characters");
    }
}
