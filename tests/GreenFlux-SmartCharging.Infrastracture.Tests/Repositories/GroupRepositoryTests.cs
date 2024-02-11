
using FluentAssertions;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using GreenFlux_SmartCharging.Infrastructure.Repositories;
using GreenFlux_SmartCharging.Tests.Common.Builders;

namespace GreenFlux_SmartCharging.Infrastracture.Tests.Repositories
{
    public class GroupRepositoryTests:TestBase
    {
        public GroupRepositoryTests()
        {
            TestDb = new TestDatabaseInitializer();
        }

        [Fact]
        public async Task GetByIdAsync_WhenDataExist_ShouldReturnCorrectData()
        {
            var groupId = Guid.NewGuid();
            var group1 = new GroupBuilder()
                .WithId(groupId)
                .WithName("g1")
                .WithCapacity(500)
                .Build();
            var group2 = new GroupBuilder()
                .WithId(Guid.NewGuid())
                .WithName("g2")
                .WithCapacity(800)
                .Build();

            await using (var dbContext = new AppDbContext(TestDb.ContextOptions))
            {
                await dbContext.AddRangeAsync(group1, group2);
                await dbContext.SaveChangesAsync();
            }

            await using(var dbContext = new AppDbContext(TestDb.ContextOptions))
            {
                var groupRepository = new GroupRepository(dbContext);
                var result = await groupRepository.GetByIdAsync(groupId);
                result.Should().NotBeNull();
                result.Name.Should().Be("g1");
                result.Capacity.Should().Be(500);
            }
        }

        [Fact]
        public async Task GetByIdAsync_WhenDataNotExist_ShouldReturnNull()
        {
            var groupId = Guid.NewGuid();
            await using (var dbContext = new AppDbContext(TestDb.ContextOptions))
            {
                var groupRepository = new GroupRepository(dbContext);
                var result = await groupRepository.GetByIdAsync(groupId);
                result.Should().BeNull();
            }
        }
    }
}
