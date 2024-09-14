// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-14-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-14-2024
// ***********************************************************************
// <copyright file="InsertPropertyHandlerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Domain.Properties.Entity;

namespace RealState.Api.Tests.Application.Properties.Command;

using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RealState.Application.Ports;
using RealState.Application.Properties.Command;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using RealState.Domain.Properties.Port;


/// <summary>
/// Defines test class InsertPropertyHandlerTests.
/// </summary>
[TestFixture]
public class InsertPropertyHandlerTests
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
    /// The unit of work mock
    /// </summary>
    private Mock<IUnitOfWork> _unitOfWorkMock;
    /// <summary>
    /// The handler
    /// </summary>
    private InsertPropertyHandler _handler;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _ownerRepositoryMock = new Mock<IOwnerQueryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new InsertPropertyHandler(
            _propertyRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    /// <summary>
    /// Defines the test method Handle_ValidRequest_InsertsPropertySuccessfully.
    /// </summary>
    [Test]
    public async Task Handle_ValidRequest_InsertsPropertySuccessfully()
    {
        // Arrange
        var command = new InsertPropertyCommand(
            "Property Name",
            "Property Address",
            150m,
            2024,
            Guid.NewGuid());

        var owner = new Owner { Id = command.IdOwner, Name = "Owner Name", Address = "Calle 25 Dg 8-20" ,Photo = "ms.png", Birthday = DateTime.Now};

        var propertyCreated = new Property { Id = Guid.NewGuid(), Name = "juan perea" };

        _ownerRepositoryMock.Setup(r => r.GetByIdAsync(command.IdOwner))
            .ReturnsAsync(owner);
        _propertyRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Property>()))
            .ReturnsAsync(propertyCreated);
        _unitOfWorkMock.Setup(u => u.SaveAsync(default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(propertyCreated.Id, Is.EqualTo(result));
        _ownerRepositoryMock.Verify(r => r.GetByIdAsync(command.IdOwner), Times.Once);
        _propertyRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Property>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(default), Times.Once);
    }

    /// <summary>
    /// Defines the test method Handle_OwnerNotFound_ThrowsException.
    /// </summary>
    /// <returns>Task.</returns>
    [Test]
    public void Handle_OwnerNotFound_ThrowsException()
    {
        // Arrange
        var command = new InsertPropertyCommand(
            "test1",
            "Property Name",
            150m,
            2024,
            IdOwner: Guid.NewGuid());

        _ownerRepositoryMock.Setup(r => r.GetByIdAsync(command.IdOwner))
            .ReturnsAsync((Owner)null);

        
       Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
    }
}
