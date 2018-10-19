using System;
using System.Collections.Generic;
using System.Text;

namespace Recycling.Model.Entities
{
    public class UserHub : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int HubId { get; set; }
        public Hub Hub { get; set; }
    }
}
