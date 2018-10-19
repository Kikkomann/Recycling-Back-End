using FluentValidation;

namespace Recycling.API.Models.Validators
{
    public class UserModelValidator : AbstractValidator<User>
    {
        public UserModelValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("First name cannot be empty");
        }
    }
}
