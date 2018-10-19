using Recycling.Data.Abstract;
using Recycling.Model.Entities;

namespace Recycling.Data.Repositories
{
    public class UserHubRepository : EntityBaseRepository<UserHub>, IUserHubRepository
    {
        public UserHubRepository(RecyclingContext context) : base(context) { }
    }
}
