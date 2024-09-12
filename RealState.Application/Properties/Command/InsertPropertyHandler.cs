using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Owners.Port;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Command
{
    internal class InsertPropertyHandler(IPropertyRepository propertyRepository, IOwnerQueryRepository ownerRepository
        , IUnitOfWork unitOfWork) : IRequestHandler<InsertPropertyCommand, Guid>
    {
        public async Task<Guid> Handle(InsertPropertyCommand request, CancellationToken cancellationToken)
        { 
            var owner = await ownerRepository.GetByIdAsync(request.IdOwner);
            owner.ValidateNull("Owner not found");

            var property = new Property
            {
                IdOwner = owner.Id, 
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                CodeInternal = Guid.NewGuid().ToString(),
                Year = request.Year
            };      

            var propertyCreated = await propertyRepository.AddAsync(property);
            await unitOfWork.SaveAsync();

            return propertyCreated.Id;
        }
    }
}
