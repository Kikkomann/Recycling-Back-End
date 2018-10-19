using System;
using System.Collections.Generic;
using System.Text;

namespace Recycling.Model.Entities
{
    public class WasteManagement : IEntityBase
    {
        public WasteManagement()
        {
            Hubs = new List<Hub>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Hub> Hubs { get; set; }
    }
}
