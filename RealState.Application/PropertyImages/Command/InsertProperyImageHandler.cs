// ***********************************************************************
// Assembly         : RealState.Application
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="InsertProperyImageHandler.cs" company="RealState.Application">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Domain.Properties.Port;
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyImages.Port;

namespace RealState.Application.PropertyImages.Command
{
    /// <summary>
    /// Class InsertProperyImageHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{RealState.Application.PropertyImages.Command.InsertPropertyImageCommand, System.Guid}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{RealState.Application.PropertyImages.Command.InsertPropertyImageCommand, System.Guid}" />
    public class InsertProperyImageHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IUnitOfWork unitOfWork) : IRequestHandler<InsertPropertyImageCommand, Guid>
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        /// <exception cref="System.IO.FileNotFoundException">Invalid file type</exception>
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

        /// <summary>
        /// Determines whether [is valid file type] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if [is valid file type] [the specified file name]; otherwise, <c>false</c>.</returns>
        private static bool IsValidFileType(string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(fileExtension) && allowedExtensions.Contains(fileExtension);
        }
    }
}
