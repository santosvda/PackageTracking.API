using AutoMapper;
using FluentAssertions;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Application.Packagess.Commands.CreatePackage;
using PackageTracking.Application.Statuses.Dtos;
using PackageTracking.Domain.Entities;
using Xunit;

namespace PackageTracking.Application.Extensions.AutoMapper.Profiles.Tests;

public class PackageProfileTests
{
    [Fact()]
    public void CreateMap_PackageToPackageDto_MapsCorrectly()
    {
        // arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PackageProfile>();
            cfg.AddProfile<StatusProfile>();
        });

        var mapper = configuration.CreateMapper();

        var package = new Package
        {
            Id = 1,
            Adress = new Adress
            {
                City = "City",
                Street = "Street",
                PostalCode = "PostalCode",
                Country = "Country"
            },
            Statuses = new List<Status>
            {
                new Status { 
                    Description = "Status1",
                    Id = 1,
                    DateTime = DateTime.Now,
                    PackageId = 1
                },
                new Status {
                    Description = "Status2",
                    Id = 2,
                    DateTime = DateTime.Now,
                    PackageId = 1
                },
            }
        };

        // act
        var packageDto = mapper.Map<PackageDto>(package);
        
        // assert
        packageDto.Should().NotBeNull();
        packageDto.City.Should().Be(package.Adress.City);
        packageDto.Street.Should().Be(package.Adress.Street);
        packageDto.PostalCode.Should().Be(package.Adress.PostalCode);
        packageDto.Country.Should().Be(package.Adress.Country);
        packageDto.Statuses.Should().NotBeNull();
        packageDto.Statuses.Should().HaveCount(package.Statuses.Count);
        packageDto.Statuses.Should().BeEquivalentTo(package.Statuses, options => options
            .ExcludingMissingMembers()
            .Using<StatusDto>(ctx => ctx.Subject.Description.Should().Be(ctx.Expectation.Description))
            .WhenTypeIs<StatusDto>());
    }

    [Fact()]
    public void CreateMap_CreatePackageCommandToPackage_MapsCorrectly()
    {
        // arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PackageProfile>();
        });
        var mapper = configuration.CreateMapper();
        var command = new CreatePackageCommand
        {
            City = "City",
            Street = "Street",
            PostalCode = "PostalCode",
            Country = "Country"
        };
        // act
        var package = mapper.Map<Package>(command);
        // assert
        package.Should().NotBeNull();
        package.Adress.Should().NotBeNull();
        package.Adress.City.Should().Be(command.City);
        package.Adress.Street.Should().Be(command.Street);
        package.Adress.PostalCode.Should().Be(command.PostalCode);
        package.Adress.Country.Should().Be(command.Country);
    }
}