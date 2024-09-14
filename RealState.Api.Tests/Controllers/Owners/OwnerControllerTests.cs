// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="OwnerControllerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealState.Api.Controllers.Owners;
using RealState.Application.Owners.Command;
using RealState.Application.Owners.Query;
using RealState.Application.Owners.Query.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Controllers.Owners
{
    /// <summary>
    /// Defines test class OwnerControllerTests.
    /// </summary>
    [TestFixture]
    public class OwnerControllerTests
    {
        /// <summary>
        /// The mock mediator
        /// </summary>
        private Mock<IMediator> _mockMediator;
        /// <summary>
        /// The controller
        /// </summary>
        private OwnerController _controller;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new OwnerController(_mockMediator.Object);
        }

        /// <summary>
        /// Defines the test method GetAll_ShouldReturnOk.
        /// </summary>
        [Test]
        public async Task GetAll_ShouldReturnOk()
        {
            // Arrange

            var owners = new List<SummaryOwnerDto>
            {
                new() { Id = Guid.NewGuid(), Name = "John Doe", Address = "123 Main St" , Photo = "https://example.com/photo.jpg", Birthday = DateTime.UtcNow},
                new() { Id = Guid.NewGuid(), Name = "Jane Doe", Address = "123 Main St" , Photo = "https://example.com/photo.jpg", Birthday = DateTime.UtcNow }
            };

            _mockMediator
             .Setup(m => m.Send(It.IsAny<GetAllOwnerQuery>(), default))
             .ReturnsAsync(owners);

            var result = await _controller.GetAll();


            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        /// <summary>
        /// Defines the test method Create_ShouldReturnCreated.
        /// </summary>
        [Test]
        public async Task Create_ShouldReturnCreated()
        {
            var newOwner = new InsertOwnerCommand()
            {
                Name = "Jane Doe",
                Address = "123 Main St",
                Photo = "img2.jpg",
                Birthday = DateTime.UtcNow
            };

            var ownerId = Guid.NewGuid();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<InsertOwnerCommand>(), default))
                .ReturnsAsync(ownerId);

            var result = await _controller.Create(newOwner);

            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.That(createdResult.Value, Is.EqualTo(ownerId));
        }
    }
}
