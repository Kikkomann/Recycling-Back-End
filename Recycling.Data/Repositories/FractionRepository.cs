using Recycling.Data.Abstract;
using Recycling.Model.Entities;

namespace Recycling.Data.Repositories
{
    public class FractionRepository : EntityBaseRepository<Fraction>, IFractionRepository
    {
        public FractionRepository(RecyclingContext context)
            : base(context)
        { }
    }
}
