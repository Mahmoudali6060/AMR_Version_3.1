using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AMR_Server.Application.DeviceGroups.Queries
{
    public partial class DeviceGroupDto : IMapFrom<DeviceGroup>
    {
        public decimal GroupId { get; set; }
        public string GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeviceGroup, DeviceGroupDto>();
        }

    }

}
