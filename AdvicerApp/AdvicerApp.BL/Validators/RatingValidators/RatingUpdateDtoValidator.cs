using AdvicerApp.BL.DTOs.RatingDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.RatingValidators;

public class RatingUpdateDtoValidator : AbstractValidator<RatingUpdateDto>
{
    public RatingUpdateDtoValidator()
    {
        RuleFor(x => x.Score)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating score must be between 1-5");
    }
}
