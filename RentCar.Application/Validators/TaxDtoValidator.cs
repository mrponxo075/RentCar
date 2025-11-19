using FluentValidation;
using RentCar.Application.Dtos.Requests;

namespace RentCar.Application.Validators
{
    public class TaxDtoValidator : AbstractValidator<TaxRequestDto>
    {
        public TaxDtoValidator()
        {
            RuleFor(t => t.TaxName)
                .NotNull().WithMessage("Tax name cannot be null.")
                .NotEmpty().WithMessage("Tax name cannot be empty.")
                .MaximumLength(100).WithMessage("Tax name maximum length is 100.");

            RuleFor(t => t.TaxDescription)
                .MaximumLength(250).WithMessage("Tax description maximum length is 250.");
            
            RuleFor(t => t.Rate)
                .GreaterThanOrEqualTo(0).WithMessage("Rate must be greater than or equal to 0.")
                .LessThanOrEqualTo(100).WithMessage("Rate must be less than or equal to 100.");
        }
    }
}
