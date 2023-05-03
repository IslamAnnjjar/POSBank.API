using FluentValidation;

namespace ElectronicsShop.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(m => m.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Id is required");
    }
}