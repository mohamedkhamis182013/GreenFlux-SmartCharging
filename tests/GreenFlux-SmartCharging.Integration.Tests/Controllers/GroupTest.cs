using System.Net;
using System.Text;
using FluentAssertions;
using GreenFlux_SmartCharging.Application.Common.Extensions;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using GreenFlux_SmartCharging.Tests.Common.Builders;
using Newtonsoft.Json;

namespace GreenFlux_SmartCharging.Integration.Tests.Controllers
{
    public class GroupTest: TestBase
    {
        [Fact]
        public async Task GettingAllGroups_WhenDataExist_ShouldReturn200OKResult()
        {
            var group1 = new GroupBuilder()
                .WithId(Guid.NewGuid())
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

            var response = await Client.GetAsync($"/api/Group/GetAll");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GettingAllGroups_WhenDataNotExist_ShouldReturn404NotFound()
        {
            var response = await Client.GetAsync($"/api/Group/GetAll");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task CreateGroup_WhenModelIsValid_ShouldReturnOkStatus()
        {
            var group1 = new GroupBuilder()
                .WithId(Guid.NewGuid())
                .WithName("g1")
                .WithCapacity(500)
                .Build();
            var groupDto = group1.ToGroupDto();
            var response = await Client.PostAsync("/api/Group/Create",
                new StringContent(JsonConvert.SerializeObject(groupDto),
                    Encoding.UTF8, "application/json")
            );
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateGroup_WhenModelIsNotValid_ShouldReturnStatus()
        {
            var group1 = new GroupBuilder()
                .WithId(Guid.NewGuid())
                .WithCapacity(500)
                .Build();
            var groupDto = group1.ToGroupDto();
            var response = await Client.PostAsync("/api/Group/Create",
                new StringContent(JsonConvert.SerializeObject(groupDto),
                    Encoding.UTF8, "application/json")
            );
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
