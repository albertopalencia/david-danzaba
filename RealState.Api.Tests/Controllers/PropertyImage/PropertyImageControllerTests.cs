// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="PropertyImageControllerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealState.Api.Controllers.PropertyImages;
using RealState.Application.PropertyImages.Command;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Controllers.PropertyImage;


/// <summary>
/// Defines test class PropertyImageControllerTests.
/// </summary>
[TestFixture]
public class PropertyImageControllerTests
{
    /// <summary>
    /// The mock mediator
    /// </summary>
    private Mock<IMediator> _mockMediator;
    /// <summary>
    /// <br />
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


        using var memoryStream = new MemoryStream();
        await file.Object.CopyToAsync(memoryStream);

        InsertPropertyImageCommand request = new() { IdProperty = idProperty, File = file.Object };
        var result = await _controller.AddImage(request);

        var createdResult = result as CreatedResult;
        Assert.IsNotNull(createdResult);
        Assert.That(createdResult.Location, Is.EqualTo($"/images/{idPropertyImage}"));
    }
}