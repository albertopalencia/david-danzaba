using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealState.Api.Controllers.PropertyImages;
using System.Threading.Tasks;
using System;
using RealState.Application.PropertyImages.Command;

namespace RealState.Api.Tests.Controllers.PropertyImage;


[TestFixture]
public class PropertyImageControllerTests
{
    /// <summary>
    /// The mock mediator
    /// </summary>
    private Mock<IMediator> _mockMediator;
    /// <summary>
    ///   <br />
    /// </summary>
    private PropertyImageController _controller;

    /// <summary>
    /// Sets up.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new PropertyImageController(_mockMediator.Object);
    }

    /// <summary>
    /// Adds the image should return created.
    /// </summary>
    [Test]
    public async Task AddImage_ShouldReturnCreated()
    {
        // Arrange
        var idProperty = Guid.NewGuid();
        var file = new Mock<IFormFile>();
        file.Setup(f => f.FileName).Returns("image.jpg");
        file.Setup(f => f.Length).Returns(1024);

        var idPropertyImage = Guid.NewGuid();
        _mockMediator
            .Setup(m => m.Send(It.IsAny<InsertPropertyImageCommand>(), default))
            .ReturnsAsync(idPropertyImage);

        // Act
        var result = await _controller.AddImage(idProperty, file.Object);

        // Assert
        var createdResult = result as CreatedResult;
        Assert.IsNotNull(createdResult);
        Assert.That(createdResult.Location, Is.EqualTo($"/images/{idPropertyImage}"));
    }
}