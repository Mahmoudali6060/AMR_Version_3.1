using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AMR_Server.Application.Gateways.Queries
{
    public partial class GatewayDto : IMapFrom<Gateway>
    {
       
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Gateway, GatewayDto>();
        }

    }
}
