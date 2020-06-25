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

namespace AMR_Server.Application.Gateways.Queries
{
    public class GetAllGatewaysQuery : IRequest<IEnumerable<SP_GetAllGateway_Result>>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string GatewayCode { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }
        public class GetAllGatewaysQueryHandler : IRequestHandler<GetAllGatewaysQuery, IEnumerable<SP_GetAllGateway_Result>>
        {
            private readonly IFunctionImports _functionImports;

            public GetAllGatewaysQueryHandler(IFunctionImports functionImports)
            {
                _functionImports = functionImports;
            }

            public async Task<IEnumerable<SP_GetAllGateway_Result>> Handle(GetAllGatewaysQuery request, CancellationToken cancellationToken)
            {
                var gateways = _functionImports.GetAllGateway(request.CurrentPage,request.PageSize,request.GatewayCode,request.VendorId,request.GroupId).ToList();
                return gateways;
            }
        }
    }

}
