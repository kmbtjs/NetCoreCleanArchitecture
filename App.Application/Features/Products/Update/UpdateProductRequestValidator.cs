using FluentValidation;

namespace App.Application.Features.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 25).WithMessage("Product name must be between 3 and 25 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than 0.");

            RuleFor(x => x.Stock)
                .InclusiveBetween(0, 1000).WithMessage("Product stock must be between 0 and 1000.");

            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
        }
    }
}
