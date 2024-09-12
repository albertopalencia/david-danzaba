using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Properties.Port;

namespace RealState.Application.Properties.Command
{
    internal class ChangePropertyPriceHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangePropertyPriceCommand, Unit>
    {
        public async Task<Unit> Handle(ChangePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            request.Price.ValidateGreaterThanZero("Price must be positive");

            var property = await propertyRepository.GetByIdAsync(request.PropertyId);
            property.ValidateNull("Property not found"); 

            property.ChangePrice(request.Price); 
            
            await propertyRepository.UpdateAsync(property);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
