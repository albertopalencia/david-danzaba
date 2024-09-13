using AutoMapper;
using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Owners.Port;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Command
{
    internal class UpdatePropertyHandler(IPropertyRepository propertyRepository, IOwnerQueryRepository ownerRepository,
        IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdatePropertyCommand, Unit>
    {
        public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyToUpdate = await propertyRepository.GetByIdAsync(request.Id);
            propertyToUpdate.ValidateNull("Property not found");

            var owner = await ownerRepository.GetByIdAsync(request.IdOwner);
            owner.ValidateNull("Owner not found");

            var propertyRequest = mapper.Map<Property>(request);
            var propertyChangeTracker = PropertyChangeTracker.CreateEntityWithChangedProperties(propertyToUpdate, propertyRequest);


            //var property = new Property
            //{
            //    IdOwner = request.IdOwner ?? owner.Id,
            //    Name = request.Name,
            //    Address = request.Address,
            //    Price = request.Price,
            //    CodeInternal = Guid.NewGuid().ToString(),
            //    Year = request.Year
            //};

            await propertyRepository.AddAsync(propertyChangeTracker);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
