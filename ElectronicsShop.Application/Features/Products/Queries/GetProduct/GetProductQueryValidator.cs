using FluentValidation;

namespace ElectronicsShop.Application.Features.Products.Queries.GetProduct;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{

    public GetProductQueryValidator()
    {

        RuleFor(m => m.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Id is required");
    }
}