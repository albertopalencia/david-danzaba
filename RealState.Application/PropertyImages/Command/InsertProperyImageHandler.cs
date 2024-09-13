using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Properties.Port;
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyImages.Port;

namespace RealState.Application.PropertyImages.Command
{
    internal class InsertProperyImageHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IUnitOfWork unitOfWork) : IRequestHandler<InsertPropertyImageCommand, Guid>
    {
        public async Task<Guid> Handle(InsertPropertyImageCommand request, CancellationToken cancellationToken)
        {
            if (!IsValidFileType(request.File.FileName))
            {
                throw new FileNotFoundException("Invalid file type");
            }

            var property = await propertyRepository.GetByIdAsync(request.IdProperty);
            property.ValidateNull("Property not found");

            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream, cancellationToken);

            PropertyImage propertyImage = new()
            {
                IdProperty = request.IdProperty,
                File = memoryStream.ToArray(),
                Enabled = true
            };

            var result = await propertyImageRepository.AddAsync(propertyImage);
            await unitOfWork.SaveAsync();
            return result.Id;
        }

        private static bool IsValidFileType(string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(fileExtension) && allowedExtensions.Contains(fileExtension);
        }
    }
}
