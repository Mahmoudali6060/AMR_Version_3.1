using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.ComplexTypes;
using AMR_Server.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Privileges.Queries.GetUserPrivileges
{
    public class GetUserPrivilegesQuery : IRequest<IEnumerable<SP_GetUserPrivileges_Result>>
    {
        public int UserId { get; set; }
    }

    public class GetUserPrivilegesQueryHandler : IRequestHandler<GetUserPrivilegesQuery, IEnumerable<SP_GetUserPrivileges_Result>>
    {

        private readonly IFunctionImports _functionImports;
        public GetUserPrivilegesQueryHandler(IFunctionImports functionImports)
        {
            _functionImports = functionImports;
        }

        public async Task<IEnumerable<SP_GetUserPrivileges_Result>> Handle(GetUserPrivilegesQuery request, CancellationToken cancellationToken)
        {
            return _functionImports.GetUserPrivileges(request.UserId);
        }
    }
}
