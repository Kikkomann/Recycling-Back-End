using FluentValidation;

namespace Recycling.API.Models.Validators
{
    public class HubModelValidator : AbstractValidator<Hub>
    {
        public HubModelValidator()
        {
            RuleFor(hub => hub.Name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}
