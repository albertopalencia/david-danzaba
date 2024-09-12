using RealState.Domain.Owners.Entity;

namespace RealState.Domain.Owners.Port
{
    public interface IOwnerQueryRepository
    {
        Task<Owner> GetByIdAsync(Guid id);

        Task<IEnumerable<Owner>> GetAllAsync();
    }
}
