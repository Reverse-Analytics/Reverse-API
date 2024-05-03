using FluentValidation;
using ReverseAnalytics.Domain.DTOs.ProductCategory;

namespace ReverseAnalytics.Domain.Validators.ProductCategory;

public class ProductCategoryForCreateValidator : AbstractValidator<ProductCategoryForCreateDto>
{
    public ProductCategoryForCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Category name cannot be empty.")
            .MinimumLength(ValidationConstants.DEFAULT_MIN_STRING_LENGTH)
            .WithMessage($"Category name must contain at least {ValidationConstants.DEFAULT_MIN_STRING_LENGTH} characters.")
            .MaximumLength(ValidationConstants.DEFAULT_MAX_STRING_LENGTH)
            .WithMessage($"Category name must contain max {ValidationConstants.DEFAULT_MAX_STRING_LENGTH} characters.");
        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.DEFAULT_LARGE_STRING_LENGTH)
            .WithMessage($"Description can contain {ValidationConstants.DEFAULT_LARGE_STRING_LENGTH} max characters");
        RuleFor(x => x.ParentId)
            .GreaterThan(0)
            .When(x => x.ParentId != null)
            .WithMessage(x => $"Parent id: {x.ParentId} is invalid");
    }
}
