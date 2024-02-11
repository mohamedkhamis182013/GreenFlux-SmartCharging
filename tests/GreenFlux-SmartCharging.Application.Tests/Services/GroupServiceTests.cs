using FluentAssertions;
using GreenFlux_SmartCharging.Application.Common.Exceptions;
using GreenFlux_SmartCharging.Application.Common.Extensions;
using GreenFlux_SmartCharging.Application.Services;
using GreenFlux_SmartCharging.Domain.Common;
using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Tests.Common.Builders;
using Moq;

namespace GreenFlux_SmartCharging.Application.Tests.Services;
public class GroupServiceTests
{
    private readonly Mock<IGroupRepository> _repository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    public GroupServiceTests()
    {
        _repository = new Mock<IGroupRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }
    [Fact]
    public async Task GetByIdAsync_WhenDataExist_ShouldReturnGroupDto()
    {
        var groupId = Guid.NewGuid();
        var group1 = new GroupBuilder()
            .WithId(groupId)
            .WithName("g1")
            .WithCapacity(500)
            .Build();
        var expectedResult = group1.ToGroupDto();
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group1);

        var sut = new GroupService( _unitOfWork.Object, _repository.Object);
        var result = await sut.GetByIdAsync(groupId);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetByIdAsync_WhenDataNotExist_ShouldThrowNotFoundException()
    {
        var groupId = Guid.NewGuid();
        var sut = new GroupService(_unitOfWork.Object, _repository.Object);
        var action = ()=>  sut.GetByIdAsync(groupId);
        await action.Should().ThrowAsync<NotFoundException>();
    }
}
