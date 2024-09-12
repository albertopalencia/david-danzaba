using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyImages.Port;

namespace RealState.Application.PropertyImages.Command
{
    internal class InsertProperyImageHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IUnitOfWork unitOfWork) : IRequestHandler<InsertPropertyImageCommand, Guid>
    {
        public async Task<Guid> Handle(InsertPropertyImageCommand request, CancellationToken cancellationToken)
        {

            Property property = await propertyRepository.GetByIdAsync(request.IdProperty);

            property.ValidateNull("Property not found");

            PropertyImage propertyImage = new()
            {
                IdProperty = request.IdProperty,
                File = request.File
            };
             
            var result = await propertyImageRepository.AddAsync(propertyImage);
            await unitOfWork.SaveAsync();
            return result.Id;
        }
    }
}
