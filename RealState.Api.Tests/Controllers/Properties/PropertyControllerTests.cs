// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-12-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="PropertyControllerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealState.Api.Controllers.Properties;
using RealState.Application.Properties.Command;
using RealState.Application.Properties.Query;
using RealState.Application.Properties.Query.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Controllers.Properties
{
    /// <summary>
    /// Defines test class PropertyControllerTests.
    /// </summary>
    [TestFixture]
    public class PropertyControllerTests
    {
        /// <summary>
        /// The mock mediator
        /// </summary>
        private Mock<IMediator> _mockMediator;
        /// <summary>
        /// The controller
        /// </summary>
        private PropertyController _controller;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new PropertyController(_mockMediator.Object);
        }

        /// <summary>
        /// Defines the test method GetAllWithFilters_ShouldReturnOk.
        /// </summary>
        [Test]
        public async Task GetAllWithFilters_ShouldReturnOk()
        {
            // Arrange
            var query = new GetAllPropertyQuery();
            IEnumerable<SummaryPropertiesDto> properties = new List<SummaryPropertiesDto>();

            _mockMediator
                .Setup(m => m.Send(query, default))
                .ReturnsAsync(properties);

            // Act
            var result = await _controller.GetAllWithFilters(query);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(properties, okResult.Value);
        }

        /// <summary>
        /// Defines the test method Create_ShouldReturnCreated.
        /// </summary>
        [Test]
        public async Task Create_ShouldReturnCreated()
        {
            // Arrange
            var command = new InsertPropertyCommand("", "", 100, 2024, Guid.NewGuid());
            var createdId = Guid.NewGuid();
            _mockMediator
                .Setup(m => m.Send(command, default))
                .ReturnsAsync(createdId);

            // Act
            var result = await _controller.Create(command);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(createdId, createdResult.Value);
        }
    }
}