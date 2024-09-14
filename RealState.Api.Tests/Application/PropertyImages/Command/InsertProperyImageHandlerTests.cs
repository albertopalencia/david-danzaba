// ***********************************************************************
// Assembly         : 
// Author           : Usuario
// Created          : 09-13-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="InsertProperyImageHandlerTests.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Moq;
using RealState.Application.Ports;
using RealState.Application.PropertyImages.Command;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;
using RealState.Domain.PropertyImages.Entity;
using RealState.Domain.PropertyImages.Port;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Application.PropertyImages.Command
{
    /// <summary>
    /// Defines test class InsertPropertyImageHandlerTests.
    /// </summary>
    [TestFixture]
    public class InsertPropertyImageHandlerTests
    {
        /// <summary>
        /// The property repository mock
        /// </summary>
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        /// <summary>
        /// The property image repository mock
        /// </summary>
        private Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
        /// <summary>
        /// The unit of work mock
        /// </summary>
        private Mock<IUnitOfWork> _unitOfWorkMock;
        /// <summary>
        /// The handler
        /// </summary>
        private InsertProperyImageHandler _handler;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new InsertProperyImageHandler(
                _propertyRepositoryMock.Object,
                _propertyImageRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        /// <summary>
        /// Defines the test method Handle_ValidFileType_ShouldAddPropertyImageAndSave.
        /// </summary>
        [Test]
        public async Task Handle_ValidFileType_ShouldAddPropertyImageAndSave()
        {
            // Arrange
            var fileName = "image.jpg";
            var fileContent = new MemoryStream([1, 2, 3]);
            var command = new InsertPropertyImageCommand
            {
                IdProperty = Guid.NewGuid(),
                File = new FormFile(fileContent, 0, fileContent.Length, "file", fileName)
            };

            var property = new Property { Id = command.IdProperty, Name = "propiedad "};
            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.IdProperty, default))
                .ReturnsAsync(property);

            using var memoryStream = new MemoryStream();
            await command.File.CopyToAsync(memoryStream);

            var propertyImage = new PropertyImage { Id = Guid.NewGuid(), IdProperty = command.IdProperty, File = memoryStream.ToArray() };

            _propertyImageRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>()))
                .ReturnsAsync(propertyImage);
 
            var result = await _handler.Handle(command, CancellationToken.None);

    
            Assert.That(result, Is.EqualTo(propertyImage.Id));
            _propertyImageRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PropertyImage>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(default), Times.Once);
        }

        /// <summary>
        /// Defines the test method Handle_InvalidFileType_ShouldThrowFileNotFoundException.
        /// </summary>
        [Test]
        public void Handle_InvalidFileType_ShouldThrowFileNotFoundException()
        {
            // Arrange
            var command = new InsertPropertyImageCommand
            {
                IdProperty = Guid.NewGuid(),
                File = new FormFile(new MemoryStream(), 0, 0, "file", "document.txt")
            };

            // Act & Assert
            Assert.ThrowsAsync<FileNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
