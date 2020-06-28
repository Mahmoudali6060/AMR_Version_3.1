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

namespace AMR_Server.Application.DeviceVendors.Queries
{
    public class GetDeviceVendorLiteQuery : IRequest<IEnumerable<DeviceVendorDto>>
    {
        public class GetDeviceVendorLiteQueryHandler : IRequestHandler<GetDeviceVendorLiteQuery, IEnumerable<DeviceVendorDto>>
        {
            private readonly IAmrDbContext _context;
            private readonly IMapper _mapper;

            public GetDeviceVendorLiteQueryHandler(IAmrDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<DeviceVendorDto>> Handle(GetDeviceVendorLiteQuery request, CancellationToken cancellationToken)
            {
                return await _context.MeterVendor
                         .Where(x => x.IsActive == true && x.DeleteStatus == false)
                         .ProjectTo<DeviceVendorDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            }
        }
    }

}
