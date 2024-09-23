using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class ProductInsertValidator : AbstractValidator<ProductInsertDto>
    {
        public ProductInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("The name must be between 2 and 20 characters long");
            RuleFor(x => x.BrandID).NotNull().WithMessage(x => "The brand is required");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "It must be a valid brand");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(x => "The {PropertyName} must be greater than 0");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage(x => "The {PropertyName} must be non-negative");
        }
    }

    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("The name must be between 2 and 20 characters long");
            RuleFor(x => x.BrandID).NotNull().WithMessage(x => "The brand is required");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "It must be a valid brand");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(x => "The {PropertyName} must be greater than 0");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage(x => "The {PropertyName} must be non-negative");
        }
    }
}
