using Recycling.Data.Abstract;
using Recycling.Model.Entities;

namespace Recycling.Data.Repositories
{
    public class HubRepository : EntityBaseRepository<Hub>, IHubRepository
    {
        public HubRepository(RecyclingContext context)
            : base(context)
        { }
    }
}
