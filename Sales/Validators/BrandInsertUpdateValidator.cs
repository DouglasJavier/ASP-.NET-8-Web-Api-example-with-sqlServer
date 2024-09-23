using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BrandInsertValidator : AbstractValidator<BrandInsertDto>
    {
        public BrandInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("The name must be between 2 and 20 characters long");

        }
    }

    public class BrandUpdateValidator : AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("The name must be between 2 and 20 characters long");

        }
    }
}
