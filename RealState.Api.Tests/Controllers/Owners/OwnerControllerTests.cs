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
    [TestFixture]
    public class OwnerControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private OwnerController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new OwnerController(_mockMediator.Object);
        }

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

        [Test]
        public async Task Create_ShouldReturnCreated()
        {
            var newOwner = new InsertOwnerCommand("Jane Doe", "123 Main St", "https://example.com/photo.jpg", DateTime.UtcNow);
            var ownerId = Guid.NewGuid();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<InsertOwnerCommand>(), default))
                .ReturnsAsync(ownerId);

            var result = await _controller.Create(newOwner);

            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(ownerId, createdResult.Value);
        }
    }
}
