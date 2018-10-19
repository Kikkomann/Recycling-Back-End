using System;
using System.Collections.Generic;

namespace Recycling.Model.Entities
{
    public class User : IEntityBase
    {
        public User()
        {
            UserHubs = new List<UserHub>();
            TrashDeliveries = new List<Fraction>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfRegistration { get; set; } = DateTime.Now;

        public ICollection<UserHub> UserHubs { get; set; }
        public ICollection<Fraction> TrashDeliveries { get; set; }
    }
}
