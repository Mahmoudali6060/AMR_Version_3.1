using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.ComplexTypes;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.DeviceGroups.Queries
{
    public class GetDeviceGroupLiteQuery : IRequest<IEnumerable<DeviceGroupDto>>
    {
        public class GetDeviceGroupLiteQueryHandler : IRequestHandler<GetDeviceGroupLiteQuery, IEnumerable<DeviceGroupDto>>
        {
            private readonly IAmrDbContext _context;
            private readonly IMapper _mapper;

            public GetDeviceGroupLiteQueryHandler(IAmrDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<DeviceGroupDto>> Handle(GetDeviceGroupLiteQuery request, CancellationToken cancellationToken)
            {
                return await _context.DeviceGroup
                         .Where(x => x.IsActive == true && x.DeleteStatus == false)
                         .ProjectTo<DeviceGroupDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            }
        }
    }

}
