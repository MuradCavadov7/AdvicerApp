using AdvicerApp.BL.DTOs.CommentDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.CommentValidator;

public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotNull()
            .NotEmpty()
            .WithMessage("Content cannot be emtpy")
            .MaximumLength(1000)
            .WithMessage("Content must be 1000 characters");

    }
}
