using RealState.Domain.Owners.Entity;

namespace RealState.Domain.Owners.Port
{
    public interface IOwnerQueryRepository
    {
        Task<IEnumerable<Owner>> GetAllAsync();
    }
}
