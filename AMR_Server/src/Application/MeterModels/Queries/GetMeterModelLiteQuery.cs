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

namespace AMR_Server.Application.MeterModels.Queries
{
    public class GetMeterModelLiteQuery : IRequest<IEnumerable<MeterModelDto>>
    {
        public class GetMeterModelLiteQueryHandler : IRequestHandler<GetMeterModelLiteQuery, IEnumerable<MeterModelDto>>
        {
            private readonly IAmrDbContext _context;
            private readonly IMapper _mapper;

            public GetMeterModelLiteQueryHandler(IAmrDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<MeterModelDto>> Handle(GetMeterModelLiteQuery request, CancellationToken cancellationToken)
            {
                return await _context.MeterModel
                         .Where(x => x.IsActive == true && x.DeleteStatus == false)
                         .ProjectTo<MeterModelDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            }
        }
    }

}
