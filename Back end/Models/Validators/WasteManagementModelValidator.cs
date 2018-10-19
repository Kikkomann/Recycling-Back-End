using FluentValidation;

namespace Recycling.API.Models.Validators
{
    public class WasteManagementModelValidator : AbstractValidator<WasteManagement>
    {
        public WasteManagementModelValidator()
        {
            RuleFor(wasteMgmt => wasteMgmt.Name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}
