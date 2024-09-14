using AutoMapper;
using Moq;
using RealState.Application.Owners.Query;
using RealState.Application.Owners.Query.Dto;
using RealState.Domain.Owners.Entity;
using RealState.Domain.Owners.Port;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealState.Api.Tests.Application.Owners.Query
{
    [TestFixture]
    public class GetAllOwnerHandlerTests
    {
        private Mock<IOwnerQueryRepository> _ownerQueryRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetAllOwnerHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _ownerQueryRepositoryMock = new Mock<IOwnerQueryRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllOwnerHandler(
                _ownerQueryRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldReturnMappedOwners()
        {
            // Arrange
            var owners = new List<Owner>
            {
                new() { Id = Guid.NewGuid(), Name = "Owner1", Address = "Address1", Photo = "img1.png", Birthday = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(),  Name = "Owner2", Address = "Address2", Photo = "img2.png", Birthday = DateTime.UtcNow }
            };

            var ownerDtos = new List<SummaryOwnerDto>
            {
                new() { Id = Guid.NewGuid(), Name = "Owner1", Address = "Address1", Photo = "img1.png", Birthday = DateTime.UtcNow},
                new() { Id = Guid.NewGuid(), Name = "Owner2", Address = "Address2", Photo = "img2.png", Birthday = DateTime.UtcNow }
            };

            _ownerQueryRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(owners);

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<SummaryOwnerDto>>(owners))
                .Returns(ownerDtos);

            var query = new GetAllOwnerQuery();

            var result = await _handler.Handle(query, CancellationToken.None);


            Assert.AreEqual(ownerDtos, result);
            _ownerQueryRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<SummaryOwnerDto>>(owners), Times.Once);
        }
    }
}