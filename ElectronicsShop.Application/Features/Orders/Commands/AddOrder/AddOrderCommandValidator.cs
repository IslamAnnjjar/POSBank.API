using FluentValidation;

namespace ElectronicsShop.Application.Features.Categories.Commands.AddCategory;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{

    public AddOrderCommandValidator()
    {
        RuleFor(m => m.CustomerName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name min length is 3")
            .MaximumLength(20).WithMessage("Name max length is 20")
            .Must(x => x.All(c => char.IsWhiteSpace(c) || char.IsLetter(c))).WithMessage("Name is not valid");

        RuleFor(m => m.CustomerPhone).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Phone is required")
            .MinimumLength(8).WithMessage("Phone min length is 8")
            .MaximumLength(15).WithMessage("Phone max length is 15")
            .Must(x => x.All(c => char.IsWhiteSpace(c) || char.IsLetter(c))).WithMessage("Phone is not valid");
    }
}