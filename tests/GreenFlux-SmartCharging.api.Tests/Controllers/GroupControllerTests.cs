using FluentAssertions;
using GreenFlux_SmartCharging.api.Controllers;
using GreenFlux_SmartCharging.Application.Common.Extensions;
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Tests.Common.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GreenFlux_SmartCharging.api.Tests.Controllers
{
    public class GroupControllerTests
    {
        [Fact]
        public async Task CreateAsync_WhenModelIsValid_ShouldReturnOKResult()
        {
            var group1 = new GroupBuilder()
                .WithId(Guid.NewGuid())
                .WithName("g1")
                .WithCapacity(500)
                .Build();
            var groupDto = group1.ToGroupDto();
            var groupServiceMock = new Mock<IGroupService>();
            var sut = new GroupController(groupServiceMock.Object);
            var actualResult = await sut.CreateAsync(groupDto);

            actualResult.Should().BeOfType<OkResult>();
        }
    }
}
