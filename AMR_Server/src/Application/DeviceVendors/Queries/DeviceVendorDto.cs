﻿using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AMR_Server.Application.DeviceVendors.Queries
{
    public partial class DeviceVendorDto : IMapFrom<MeterVendor>
    {
        public short VendorId { get; set; }
        public string VendorName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MeterVendor, DeviceVendorDto>();
        }

    }

}
