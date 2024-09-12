using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using RealState.Infrastructure.Ports;

namespace RealState.Infrastructure.Adapters
{
    [Repository]
    public class OwnerRepository(IRepository<Owner> repository) : IOwnerRepository
    {
        public async Task<Guid> AddAsync(Owner owner)
        {
            var ownerNew = await repository.AddAsync(owner);
            return ownerNew.Id;
        }

    }
}
