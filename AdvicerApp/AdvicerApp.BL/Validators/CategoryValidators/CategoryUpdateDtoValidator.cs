using AdvicerApp.BL.DTOs.CategoryDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.CategoryValidators;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Category name cannot be empty")
            .MaximumLength(32)
            .WithMessage("Category name can be maximum 32 characters");
    }
}
