

using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

public class CheckoutOrderCommandValidator :AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(o=>o.UserName)
            .NotEmpty()
            .WithMessage("{Username} is required")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{UserName} must not exceed 70 characters");

        RuleFor(o => o.TotalPrice)
            .NotEmpty()
            .WithMessage("{TotalPrice} is required")
            .GreaterThan(-1)
            .WithMessage("{Total PRice} should not be -ve");
        RuleFor(o => o.EmailAddress)
            .NotEmpty()
            .NotNull()
            .WithMessage("{EmailAddress} is required");
        RuleFor(o=>o.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{FirstName} is required");

    }
}
