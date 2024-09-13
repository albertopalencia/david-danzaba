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
    [TestFixture]
    public class PropertyControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private PropertyController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new PropertyController(_mockMediator.Object);
        }

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