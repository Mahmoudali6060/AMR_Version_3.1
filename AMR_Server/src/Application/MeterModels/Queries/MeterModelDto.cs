﻿using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AMR_Server.Application.MeterModels.Queries
{
    public partial class MeterModelDto : IMapFrom<MeterModel>
    {
        public short ModelId { get; set; }
        public string ModelName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MeterModel, MeterModelDto>();
        }

    }

}
