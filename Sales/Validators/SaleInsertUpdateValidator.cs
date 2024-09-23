using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class SaleInsertUpdateValidator : AbstractValidator<SaleInsertUpdateDto>
    {
        public SaleInsertUpdateValidator()
        {
            // Validar que la fecha no esté vacía
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("The date is required")
                .Must(BeAValidDate).WithMessage("The date is invalid");

            // Validar que el nombre del cliente no esté vacío
            RuleFor(x => x.ClientFistName)
                .NotEmpty().WithMessage("Client's first name is required")
                .Length(2, 100).WithMessage("Client's first name must be between 2 and 100 characters.");

            // Validar que el apellido del cliente, si está presente, tenga una longitud mínima
            RuleFor(x => x.ClientLastName)
                .MaximumLength(100).WithMessage("Client's last name cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.ClientLastName));

            // Validar que el ID de documento del cliente, si está presente, tenga un formato adecuado
            RuleFor(x => x.ClientIDDocument)
                .MaximumLength(20).WithMessage("Client ID document cannot exceed 20 characters.")
                .When(x => !string.IsNullOrEmpty(x.ClientIDDocument));

            // Validar que haya al menos un producto comprado
            RuleFor(x => x.Carts)
                .NotEmpty().WithMessage("At least one product must be purchased.");

            // Validar cada cart (producto comprado)
            RuleForEach(x => x.Carts).ChildRules(carts =>
            {
                // Validar que el ID del producto sea válido
                carts.RuleFor(cart => cart.ProductID)
                    .GreaterThan(0).WithMessage("A valid ProductID is required.");

                // Validar que la cantidad del producto no sea negativa ni cero
                carts.RuleFor(cart => cart.Quantity)
                    .GreaterThan(0).WithMessage("Product quantity must be greater than zero.");

            });
        }

        // Validación personalizada para la fecha (puedes ajustarla según las necesidades)
        private bool BeAValidDate(DateTime date)
        {
            return date != default(DateTime);
        }
    }
}
