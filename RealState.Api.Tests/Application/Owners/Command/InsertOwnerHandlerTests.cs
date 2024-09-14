// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-13-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="InsertOwnerHandlerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Moq;
using RealState.Application.Owners.Command;
using RealState.Application.Ports;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Application.Owners.Command;


/// <summary>
/// Defines test class InsertOwnerHandlerTests.
/// </summary>
[TestFixture]
public class InsertOwnerHandlerTests
{
    /// <summary>
    /// The owner repository mock
    /// </summary>
    private Mock<IOwnerRepository> _ownerRepositoryMock;
    /// <summary>
    /// The unit of work mock
    /// </summary>
    private Mock<IUnitOfWork> _unitOfWorkMock;
    /// <summary>
    /// The handler
    /// </summary>
    private InsertOwnerHandler _handler;

    /// <summary>
    /// Sets up.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new InsertOwnerHandler(
            _ownerRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    /// <summary>
    /// Defines the test method Handle_ValidRequest_ShouldAddOwnerAndSave.
    /// </summary>
    [Test]
    public async Task Handle_ValidRequest_ShouldAddOwnerAndSave()
    {
        // Arrange
        var command = new InsertOwnerCommand
        {
            Name = "John Doe",
            Address = "123 Main St",
            Photo = "img.jpg",
            Birthday = new DateTime(1980, 1, 1)
        };

        var ownerId = Guid.NewGuid();
        _ownerRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Owner>()))
            .ReturnsAsync(ownerId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(ownerId));

        _ownerRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Owner>(o =>
            o.Name == command.Name &&
            o.Address == command.Address &&
            o.Photo == command.Photo &&
            o.Birthday == command.Birthday
        )), Times.Once);

        _unitOfWorkMock.Verify(uow => uow.SaveAsync(default), Times.Once);
    }

    /// <summary>
    /// Defines the test method Handle_NullOwner_ShouldThrowException.
    /// </summary>
    [Test]
    public void Handle_NullOwner_ShouldThrowException()
    {
        // Arrange
        var command = new InsertOwnerCommand
        {
            Name = null,
            Address = "123 Main St",
            Photo = "imag.png    ",
            Birthday = new DateTime(1980, 1, 1)
        };

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _handler.Handle(command, CancellationToken.None));
    }

}