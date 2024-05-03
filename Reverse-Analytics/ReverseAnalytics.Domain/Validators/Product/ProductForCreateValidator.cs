using FluentValidation;
using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Domain.Validators.Product;

public class ProductForCreateValidator : AbstractValidator<ProductForCreateDto>
{
    public ProductForCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Product name must contain at least 5 characters.")
            .MaximumLength(255)
            .WithMessage("Product name must contain maximum 255 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(4000)
            .WithMessage("Description can contain 4000 max characters.");
        RuleFor(x => x.SalePrice)
            .GreaterThan(0)
            .WithMessage($"Sale price must be great than 0.");
        RuleFor(x => x.SupplyPrice)
            .GreaterThan(0)
            .WithMessage($"Supply price must be greater than 0.")
            .LessThan(x => x.SalePrice)
            .WithMessage(x => $"Supply price: {x.SupplyPrice} cannot be less than Sale price: {x.SalePrice}.");
        RuleFor(x => x.UnitOfMeasurement)
            .IsInEnum()
            .WithMessage(x => $"Invalid Unit of measurement: {x.UnitOfMeasurement}.");
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage(x => $"Invalid category id: {x.CategoryId}.");
    }
}
