using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;

namespace RealState.Application.Owners.Command
{
    internal class InsertOwnerHandler(IOwnerRepository ownerRepository, IUnitOfWork unitOfWork) : IRequestHandler<InsertOwnerCommand, Guid>
    {
        public async Task<Guid> Handle(InsertOwnerCommand request, CancellationToken cancellationToken)
        {
            Owner owner = new()
            {
                Address = request.Address,
                Name = request.Name,
                Photo = request.Photo,
                Birthday = request.Birthday
            };

            owner.ValidateNull("the owner cannot be null");

            var result = await ownerRepository.AddAsync(owner);
            await unitOfWork.SaveAsync();
            return result;
        }
    }
}
