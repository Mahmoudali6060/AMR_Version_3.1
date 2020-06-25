using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.ComplexTypes;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Meters.Queries
{
    public class GetAllMetersQuery : IRequest<IEnumerable<SP_GetAllMeter_Result>>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string HCN { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }

        public class GetAllMetersQueryHandler : IRequestHandler<GetAllMetersQuery, IEnumerable<SP_GetAllMeter_Result>>
        {
            private readonly IFunctionImports _functionImports;
            public GetAllMetersQueryHandler(IFunctionImports functionImports)
            {
                _functionImports = functionImports;
            }

            public async Task<IEnumerable<SP_GetAllMeter_Result>> Handle(GetAllMetersQuery request, CancellationToken cancellationToken)
            {
                var meters = _functionImports.GetAllMeter(request.CurrentPage, request.PageSize,request.HCN,request.VendorId,request.GroupId).ToList();
                return meters;
            }
        }
    }

}
