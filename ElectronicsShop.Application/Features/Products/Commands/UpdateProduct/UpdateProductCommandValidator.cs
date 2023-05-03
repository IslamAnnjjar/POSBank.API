using FluentValidation;

namespace ElectronicsShop.Application.Features.Products.Commands.AddProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{

    public UpdateProductCommandValidator()
    {
        RuleFor(m => m.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(m => m.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name min length is 3")
            .MaximumLength(20).WithMessage("Name max length is 20");

        RuleFor(m => m.Price).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price should be positive");
    }
}