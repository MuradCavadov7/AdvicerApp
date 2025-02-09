using AdvicerApp.BL.DTOs.CommentDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.CommentValidator;

internal class CommentUpdateDtoValidator : AbstractValidator<CommentUpdateDto>
{
    public CommentUpdateDtoValidator()
    {
        RuleFor(x => x.Text)
           .NotNull()
           .NotEmpty()
           .WithMessage("Content cannot be emtpy")
           .MaximumLength(1000)
           .WithMessage("Content must be 1000 characters");
    }
}
