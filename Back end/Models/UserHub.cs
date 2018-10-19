
namespace Recycling.API.Models
{
    public class UserHub
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int HubId { get; set; }
        public Hub Hub { get; set; }
    }
}
