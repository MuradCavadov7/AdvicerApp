using AdvicerApp.BL.DTOs.RatingDto;
using FluentValidation;

namespace AdvicerApp.BL.Validators.RatingValidators;

public class RatingCreateDtoValidator : AbstractValidator<RatingCreateDto>
{
    public RatingCreateDtoValidator()
    {
        RuleFor(x => x.Score)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating score must be between 1-5");
    }
}
