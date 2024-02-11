using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Application.Common.Extensions;

public static class GroupExtension
{
    public static IEnumerable<GroupDto> ToGroupDtoList(this IEnumerable<Group> groupList)
    {
        var groupDtoList = new List<GroupDto>();
        foreach (var group in groupList)
        {
            if (group.ChargeStations ==null || group.ChargeStations?.Count == 0)
            {
                group.ChargeStations = new List<ChargeStation>();
            }
            groupDtoList.Add(
                new GroupDto(
                    group.Id
                    , group.Name
                    , group.Capacity
                    , group.ChargeStations.ToChargeStationDtoList().ToList()));
        }
        return groupDtoList;

    }
    public static GroupDto ToGroupDto(this Group group)
    {
        if (group.ChargeStations == null || group.ChargeStations?.Count == 0)
        {
            group.ChargeStations = new List<ChargeStation>();
        }
        return new GroupDto(
            group.Id
            , group.Name
            , group.Capacity
            , group.ChargeStations.ToChargeStationDtoList().ToList());
    }
    public static Group ToGroup(this GroupDto groupDto)
    {
        return new Group(
            groupDto.Id
            , groupDto.Name
            , groupDto.Capacity
            , groupDto.ChargeStations.ToChargeStationList());
    }
}

