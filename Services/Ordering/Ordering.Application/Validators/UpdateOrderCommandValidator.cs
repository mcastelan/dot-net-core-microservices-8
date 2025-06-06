using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validators
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Id} is required")
                .GreaterThan(0)
                .WithMessage("{Id} cannot be -ve");
            RuleFor(o => o.UserName)
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
            RuleFor(o => o.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("{FirstName} is required");
        }
    }
}
