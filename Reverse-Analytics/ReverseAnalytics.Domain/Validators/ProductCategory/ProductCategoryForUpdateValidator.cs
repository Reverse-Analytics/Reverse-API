using FluentValidation;
using ReverseAnalytics.Domain.DTOs.ProductCategory;

namespace ReverseAnalytics.Domain.Validators.ProductCategory;

public class ProductCategoryForUpdateValidator : AbstractValidator<ProductCategoryForUpdateDto>
{
    public ProductCategoryForUpdateValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name cannot be empty.")
            .MinimumLength(ValidationConstants.DEFAULT_MIN_STRING_LENGTH)
            .WithMessage("Category name must contain at least 3 characters.")
            .MaximumLength(ValidationConstants.DEFAULT_MAX_STRING_LENGTH)
            .WithMessage("Category name must contain max 255 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.DEFAULT_LARGE_STRING_LENGTH)
            .WithMessage("Description can contain 4000 max characters");
        RuleFor(x => x.ParentId)
            .GreaterThan(0)
            .When(x => x.ParentId != null)
            .WithMessage(x => $"Parent id: {x.ParentId} is invalid");
    }
}
