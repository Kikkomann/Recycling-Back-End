using FluentValidation;

namespace Recycling.API.Models.Validators
{
    public class FractionModelValidator : AbstractValidator<Fraction>
    {
        public FractionModelValidator()
        {
            RuleFor(fraction => fraction.User).NotEmpty().WithMessage("User cannot be empty");
            RuleFor(fraction => fraction.Hub).NotEmpty().WithMessage("Hub cannot be empty");
        }
    }
}
