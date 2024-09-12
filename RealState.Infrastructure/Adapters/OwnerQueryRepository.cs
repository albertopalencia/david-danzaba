using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    [Repository]
    public class OwnerQueryRepository(IRepository<Owner> ownerRepository) : IOwnerQueryRepository
    {
        public async Task<Owner> GetByIdAsync(Guid id) => await ownerRepository.GetOneAsync(id);

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await ownerRepository.GetManyAsync();
        }
    }
}
