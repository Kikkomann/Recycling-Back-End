
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Recycling.API.Models.Validators;

namespace Recycling.API.Models
{
    public class WasteManagement : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Hubs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new WasteManagementModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
