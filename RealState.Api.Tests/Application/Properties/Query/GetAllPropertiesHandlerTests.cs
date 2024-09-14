// ***********************************************************************
// Assembly         : RealState.Api.Tests
// Author           : Usuario
// Created          : 09-14-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-14-2024
// ***********************************************************************
// <copyright file="GetAllPropertiesHandlerTests.cs" company="RealState.Api.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using Moq;
using RealState.Application.Properties.Query.Dto;
using RealState.Application.Properties.Query;
using RealState.Domain.Properties.Dto;
using RealState.Domain.Properties.Port;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using RealState.Domain.Properties.Entity;

namespace RealState.Api.Tests.Application.Properties.Query;

/// <summary>
/// Defines test class GetAllPropertiesHandlerTests.
/// </summary>
[TestFixture]
public class GetAllPropertiesHandlerTests
{
    /// <summary>
    /// The property repository mock
    /// </summary>
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    /// <summary>
    /// The mapper mock
    /// </summary>
    private Mock<IMapper> _mapperMock;
    /// <summary>
    /// The handler
    /// </summary>
    private GetAllPropertiesHandler _handler;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllPropertiesHandler(_propertyRepositoryMock.Object, _mapperMock.Object);
    }

    /// <summary>
    /// Defines the test method Handle_WithValidRequest_ReturnsMappedProperties.
    /// </summary>
    [Test]
    public async Task Handle_WithValidRequest_ReturnsMappedProperties()
    {
        // Arrange
        var query = new GetAllPropertyQuery
        {
            Name = "Test Property",
            Address = "123 Test St",
            MinPrice = 100000,
            MaxPrice = 200000,
            MinYear = 2000,
            MaxYear = 2020,
            Page = 1,
            Size = 10,
            OwnerId = "some-guid"
        };

        var filterDto = new PropertyFilterQueryDto
        {
            Name = query.Name,
            Address = query.Address,
            MinPrice = query.MinPrice,
            MaxPrice = query.MaxPrice,
            MinYear = query.MinYear,
            MaxYear = query.MaxYear,
            OwnerId = query.OwnerId
        };

        var properties = new List<Property>
        {
            new() { Id = Guid.NewGuid(), Name = "Test Property", Address = "123 Test St", Price = 150000, Year = 2010, IdOwner = Guid.NewGuid() }
        };

        var summaryProperties = new List<SummaryPropertiesDto>
        {
            new()
            {
                Id = properties[0].Id,
                Name = properties[0].Name,
                Address = "123 Test St " 
            }
        };

        _mapperMock.Setup(m => m.Map<PropertyFilterQueryDto>(It.IsAny<GetAllPropertyQuery>())).Returns(filterDto);
        _propertyRepositoryMock.Setup(r => r.GetPropertiesAsync(filterDto)).ReturnsAsync(properties);
        _mapperMock.Setup(m => m.Map<IEnumerable<SummaryPropertiesDto>>(properties)).Returns(summaryProperties);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(summaryProperties.Count,Is.EqualTo(result.Count()));
        Assert.That(summaryProperties[0].Id, Is.EqualTo(result.First().Id));
    }

    /// <summary>
    /// Defines the test method Handle_WithEmptyProperties_ReturnsEmptyList.
    /// </summary>
    [Test]
    public async Task Handle_WithEmptyProperties_ReturnsEmptyList()
    {
       
        var query = new GetAllPropertyQuery();
        var filterDto = new PropertyFilterQueryDto();
        var properties = new List<Property>();
        var summaryProperties = new List<SummaryPropertiesDto>();

        _mapperMock.Setup(m => m.Map<PropertyFilterQueryDto>(It.IsAny<GetAllPropertyQuery>())).Returns(filterDto);
        _propertyRepositoryMock.Setup(r => r.GetPropertiesAsync(filterDto)).ReturnsAsync(properties);
        _mapperMock.Setup(m => m.Map<IEnumerable<SummaryPropertiesDto>>(properties)).Returns(summaryProperties);

       
        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    /// <summary>
    /// Defines the test method Handle_ThrowsException_MapsToError.
    /// </summary>
    [Test]
    public void Handle_ThrowsException_MapsToError()
    {
       
        var query = new GetAllPropertyQuery();
        var filterDto = new PropertyFilterQueryDto();

        _mapperMock.Setup(m => m.Map<PropertyFilterQueryDto>(It.IsAny<GetAllPropertyQuery>())).Returns(filterDto);
        _propertyRepositoryMock.Setup(r => r.GetPropertiesAsync(filterDto)).ThrowsAsync(new Exception("Database error"));

       
        Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(query, CancellationToken.None));
    }
}