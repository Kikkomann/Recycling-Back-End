using Recycling.Data.Abstract;
using Recycling.Model.Entities;

namespace Recycling.Data.Repositories
{
    public class WasteManagementRepository : EntityBaseRepository<WasteManagement>, IWasteManagementRepository
    {
        public WasteManagementRepository(RecyclingContext context)
            : base(context)
        { }
    }
}
