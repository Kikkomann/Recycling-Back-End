using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recycling.Model.Entities
{
    public class Fraction : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Weight { get; set; }
        public bool IsClean { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int HubId { get; set; }
        public Hub Hub { get; set; }
    }
}
