
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Recycling.API.Models.Validators;

namespace Recycling.API.Models
{
    public class Fraction : IValidatableObject
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public bool IsClean { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int HubId { get; set; }
        public string Hub { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new FractionModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
