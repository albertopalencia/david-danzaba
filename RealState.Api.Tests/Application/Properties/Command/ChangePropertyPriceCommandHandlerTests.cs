// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="ChangePropertyPriceCommandHandlerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Moq;
using RealState.Application.Ports;
using RealState.Application.Properties.Command;
using RealState.Domain.Exceptions;
using RealState.Domain.Properties.Entity;
using RealState.Domain.Properties.Port;
using RealState.Infrastructure.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Application.Properties.Command;

/// <summary>
/// Defines test class ChangePropertyPriceCommandHandlerTests.
/// </summary>
[TestFixture]
public class ChangePropertyPriceCommandHandlerTests
{
    /// <summary>
    /// The mock property repository
    /// </summary>
    private Mock<IPropertyRepository> _mockPropertyRepository;
    /// <summary>
    /// The mock repository
    /// </summary>
    private Mock<IRepository<Property>> _mockRepository;
    /// <summary>
    /// The mock unit of work
    /// </summary>
    private Mock<IUnitOfWork> _mockUnitOfWork;
    /// <summary>
    /// The handler
    /// </summary>
    private ChangePropertyPriceHandler _handler;

    /// <summary>
    /// Sets up.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mockPropertyRepository = new Mock<IPropertyRepository>();
        _mockRepository = new Mock<IRepository<Property>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new ChangePropertyPriceHandler(_mockPropertyRepository.Object, _mockUnitOfWork.Object);
    }

    /// <summary>
    /// Defines the test method Handle_ShouldChangePropertyPrice.
    /// </summary>
    [Test]
    public async Task Handle_ShouldChangePropertyPrice()
    {
        var propertyId = Guid.NewGuid();
        var newPrice = 123.45m;
        var property = new Property { Id = propertyId, Price = newPrice, Name = "juan"};

        _mockPropertyRepository.Setup(s => s.GetByIdAsync(propertyId, It.IsAny<string>())).ReturnsAsync(property);

        _mockRepository.Setup(s => s.GetOneAsync(propertyId, It.IsAny<string>())).ReturnsAsync(property);

        await _handler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None);

        _mockPropertyRepository.Verify(r => r.UpdateAsync(property), Times.Once);
    }

    /// <summary>
    /// Defines the test method Handle_ShouldChangePropertyPrice_ValidateGreaterThanZero.
    /// </summary>
    [Test]
    public void Handle_ShouldChangePropertyPrice_ValidateGreaterThanZero()
    {
        var propertyId = Guid.NewGuid();
        const int newPrice = 0;
        var property = new Property { Id = propertyId, Price = newPrice, Name = "juan"};

        _mockPropertyRepository.Setup(s => s.GetByIdAsync(propertyId, It.IsAny<string>())).ReturnsAsync(property);

        Assert.ThrowsAsync<RequiredException>(async () =>
        await _handler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None));
    }

    /// <summary>
    /// Defines the test method Handle_PropertyNotFound_ShouldThrowRequiredException.
    /// </summary>
    [Test]
    public void Handle_PropertyNotFound_ShouldThrowRequiredException()
    {

        var propertyId = Guid.NewGuid();
        const decimal newPrice = 123.45m;

        _mockPropertyRepository
            .Setup(r => r.GetByIdAsync(propertyId, It.IsAny<string>()))
            .ReturnsAsync((Property)null);


        Assert.ThrowsAsync<RequiredException>(async () =>
            await _handler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None));
    }
}