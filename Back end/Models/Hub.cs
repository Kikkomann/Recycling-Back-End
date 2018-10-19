
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Recycling.API.Models.Validators;

namespace Recycling.API.Models
{
    public class Hub : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int WasteManagementUserId { get; set; }
        public string WasteManagement { get; set; }
        public double CleanPercentage { get; set; }

        public int[] TotalFractions { get; set; }
        public int[] UserHubs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new HubModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
