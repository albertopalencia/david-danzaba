// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-14-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-14-2024
// ***********************************************************************
// <copyright file="UpdatePropertyHandlerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using Moq;
using RealState.Application.Ports;
using RealState.Application.Properties.Command;
using RealState.Domain.Exceptions;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Application.Properties.Command;


/// <summary>
/// Defines test class UpdatePropertyHandlerTests.
/// </summary>
[TestFixture]
public class UpdatePropertyHandlerTests
{
    /// <summary>
    /// The property repository mock
    /// </summary>
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    /// <summary>
    /// The owner repository mock
    /// </summary>
    private Mock<IOwnerQueryRepository> _ownerRepositoryMock;
    /// <summary>
    /// The mapper mock
    /// </summary>
    private Mock<IMapper> _mapperMock;
    /// <summary>
    /// The unit of work mock
    /// </summary>
    private Mock<IUnitOfWork> _unitOfWorkMock;
    /// <summary>
    /// The handler
    /// </summary>
    private UpdatePropertyHandler _handler;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _ownerRepositoryMock = new Mock<IOwnerQueryRepository>();
        _mapperMock = new Mock<IMapper>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new UpdatePropertyHandler(
            _propertyRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _mapperMock.Object,
            _unitOfWorkMock.Object);
    }

    /// <summary>
    /// Defines the test method Handle_ValidRequest_UpdatesPropertySuccessfully.
    /// </summary>
    [Test]
    public void Handle_ValidRequest_UpdatesPropertySuccessfully()
    {
        
        var command = new UpdatePropertyCommand(
            Guid.NewGuid(),
            "Updated Name",
            "Updated Address",
            200m,
            2024,
            Guid.NewGuid());

        var existingProperty = new Property { Id = command.Id, Name = "juan perea"};
        var owner = new Owner() { Id = command.IdOwner, Name = "Owner Name", Address = "Calle 25 Dg 8-20", Photo = "ms.png", Birthday = DateTime.Now };
            
        var propertyRequest = new Property { Id = command.Id, Name = "juan perea" };

        _propertyRepositoryMock.Setup(r => r.GetByIdAsync(command.Id,default))
            .ReturnsAsync(existingProperty);
        _ownerRepositoryMock.Setup(r => r.GetByIdAsync(command.IdOwner))
            .ReturnsAsync(owner);
        _mapperMock.Setup(m => m.Map<Property>(command))
            .Returns(propertyRequest);
        _propertyRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Property>()))
            .Returns((Task<Property>)Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveAsync(default))
            .Returns(Task.CompletedTask);

        // Act
         _handler.Handle(command, CancellationToken.None);

        // Assert
        _propertyRepositoryMock.Verify(r => r.GetByIdAsync(command.Id, default), Times.Once);
        _ownerRepositoryMock.Verify(r => r.GetByIdAsync(command.IdOwner), Times.Once);
        _mapperMock.Verify(m => m.Map<Property>(command), Times.Once);
        _propertyRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Property>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(default), Times.Once);
    }

    /// <summary>
    /// Defines the test method Handle_PropertyNotFound_ThrowsException.
    /// </summary>
    [Test]
    public void Handle_PropertyNotFound_ThrowsException()
    {
        
        var command = new UpdatePropertyCommand(
            Guid.NewGuid(),
            "Name",
            "Address",
            100m,
            2024,
            Guid.NewGuid());

        _propertyRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, default))
            .ReturnsAsync((Property)null);
 
        var ex = Assert.ThrowsAsync<RequiredException>(async () => await _handler.Handle(command, CancellationToken.None))!;
         
    }

    /// <summary>
    /// Defines the test method Handle_OwnerNotFound_ThrowsException.
    /// </summary>
    [Test]
    public void Handle_OwnerNotFound_ThrowsException()
    {
        // Arrange
        var command = new UpdatePropertyCommand(
            Guid.NewGuid(),
            "Name",
            "Address",
            100m,
            2024,
            Guid.NewGuid());

        var existingProperty = new Property { Id = command.Id, Name = "juan perea" };

        _propertyRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, default))
            .ReturnsAsync(existingProperty);
        _ownerRepositoryMock.Setup(r => r.GetByIdAsync(command.IdOwner))
            .ReturnsAsync((Owner)null);

        Assert.ThrowsAsync<RequiredException>(async () => await _handler.Handle(command, CancellationToken.None));
    }
}