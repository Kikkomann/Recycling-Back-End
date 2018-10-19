using System;
using System.Collections.Generic;
using System.Text;

namespace Recycling.Model.Entities
{
    public class Hub : IEntityBase
    {
        public Hub()
        {
            TotalFractions = new List<Fraction>();
            UserHubs = new List<UserHub>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int WasteManagementId { get; set; }
        public WasteManagement WasteManagement { get; set; }
        public double CleanPercentage { get; set; }

        public ICollection<Fraction> TotalFractions { get; set; }
        public ICollection<UserHub> UserHubs { get; set; }
    }
}
