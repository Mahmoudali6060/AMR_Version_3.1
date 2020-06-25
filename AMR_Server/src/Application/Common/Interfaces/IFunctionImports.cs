using AMR_Server.Domain.ComplexTypes;
using AMR_Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMR_Server.Application.Common.Interfaces
{
    public interface IFunctionImports
    {
        public IEnumerable<SP_GetAllMeter_Result> GetAllMeter(int currentPage, int pageSize, string HCN, short? vendorId, decimal? groupId);
        public IEnumerable<SP_GetAllGateway_Result> GetAllGateway(int currentPage, int pageSize, string gatewayCode, short? vendorId, decimal? groupId);
        public IEnumerable<SP_GetUserPrivileges_Result> GetUserPrivileges(int userId);
        public IEnumerable<SP_GetAllVendorLite_Result> GetAllVendorLite();
    }
}
