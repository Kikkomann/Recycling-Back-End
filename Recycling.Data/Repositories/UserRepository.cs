using Recycling.Data.Abstract;
using Recycling.Model.Entities;

namespace Recycling.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(RecyclingContext context) : base(context){ }
    }
}
