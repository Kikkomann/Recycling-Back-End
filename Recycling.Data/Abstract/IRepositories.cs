using Recycling.Model.Entities;

namespace Recycling.Data.Abstract
{
    public interface IHubRepository : IEntityBaseRepository<Hub> { }

    public interface IUserRepository : IEntityBaseRepository<User> { }

    public interface IFractionRepository : IEntityBaseRepository<Fraction> { }

    public interface IWasteManagementRepository : IEntityBaseRepository<WasteManagement> { }

    public interface IUserHubRepository : IEntityBaseRepository<UserHub> { }
}
