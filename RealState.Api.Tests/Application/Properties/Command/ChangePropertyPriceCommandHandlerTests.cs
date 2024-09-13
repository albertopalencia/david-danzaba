using Moq;
using NUnit.Framework;
using RealState.Application.Ports;
using RealState.Application.Properties.Command;
using RealState.Domain.Properties.Port;
using System;
using System.Threading;
using System.Threading.Tasks;
using RealState.Domain.Properties.Entity;
using RealState.Infrastructure.Ports;
using RealState.Domain.Exceptions;

namespace RealState.Api.Tests.Application.Properties.Command;

[TestFixture]
public class ChangePropertyPriceCommandHandlerTests
{
    private Mock<IPropertyRepository> _mockPropertyRepository;
    private Mock<IRepository<Property>> _mockRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private ChangePropertyPriceHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockPropertyRepository = new Mock<IPropertyRepository>();
        _mockRepository = new Mock<IRepository<Property>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new ChangePropertyPriceHandler(_mockPropertyRepository.Object, _mockUnitOfWork.Object);
    }

    [Test]
    public async Task Handle_ShouldChangePropertyPrice()
    {
        var propertyId = Guid.NewGuid();
        var newPrice = 123.45m; 
        var property = new Property { Id = propertyId, Price = newPrice };

        _mockPropertyRepository.Setup(s => s.GetByIdAsync(propertyId, It.IsAny<string>())).ReturnsAsync(property);

        _mockRepository.Setup(s => s.GetOneAsync(propertyId, It.IsAny<string>())).ReturnsAsync(property);
          
        await _handler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None); 

        _mockPropertyRepository.Verify(r => r.UpdateAsync(property), Times.Once);
    }

    [Test]
    public void Handle_ShouldChangePropertyPrice_ValidateGreaterThanZero()
    {
        var propertyId = Guid.NewGuid();
        const int newPrice = 0;
        var property = new Property { Id = propertyId, Price = newPrice };

        _mockPropertyRepository.Setup(s => s.GetByIdAsync(propertyId, It.IsAny<string>())).ReturnsAsync(property);

        Assert.ThrowsAsync<RequiredException>( async () => 
        await _handler.Handle(new ChangePropertyPriceCommand(propertyId, newPrice), CancellationToken.None)); 
    }

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