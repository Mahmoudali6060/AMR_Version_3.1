using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Application.Meters.Queries;
using AMR_Server.Domain.ComplexTypes;
using AMR_Server.Domain.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMR_Server.Infrastructure.Persistence.FunctionImports
{
    public class FunctionImports : IFunctionImports
    {
        private readonly AmrDbContext _context;//to Access other tables in context such as UserBasicData

        public FunctionImports(AmrDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SP_GetAllGateway_Result> GetAllGateway(int currentPage, int pageSize, string gatewayCode, short? vendorId, decimal? groupId)
        {
            OracleParameter p1 = new OracleParameter("GATEWAY_CODE_PARAM", OracleDbType.Varchar2, gatewayCode, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("GROUP_ID_PARAM", OracleDbType.Int32, groupId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("VENDOR_ID_PARAM", OracleDbType.Int32, vendorId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("PAGE_NO", OracleDbType.Int32, currentPage, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("PAGE_SIZE", OracleDbType.Int32, pageSize, ParameterDirection.Input);

            OracleParameter p6 = new OracleParameter("C1", OracleDbType.RefCursor, ParameterDirection.Output);
            string sql = "BEGIN SP_GETALLGATEWAY(:GATEWAY_CODE_PARAM,:GROUP_ID_PARAM,:VENDOR_ID_PARAM,:PAGE_NO,:PAGE_SIZE,:C1);END;";
            var result = _context.SP_GetAllGateway_Result.FromSqlRaw(sql, p1, p2, p3, p4, p5, p6);//[To-DO]Meter List is returned
            return result;
        }


        public IEnumerable<SP_GetAllMeter_Result> GetAllMeter(int currentPage, int pageSize, string HCN, short? vendorId, decimal? groupId)
        {
            OracleParameter p1 = new OracleParameter("HCN_PARAM", OracleDbType.Varchar2, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("GROUP_ID_PARAM", OracleDbType.Int32, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("VENDOR_ID_PARAM", OracleDbType.Int32, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("PAGE_NO", OracleDbType.Int32, currentPage, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("PAGE_SIZE", OracleDbType.Int32, pageSize, ParameterDirection.Input);

            OracleParameter p6 = new OracleParameter("C2", OracleDbType.RefCursor, ParameterDirection.Output);
            string sql = "BEGIN SP_GETALLMETER(:HCN_PARAM,:GROUP_ID_PARAM,:VENDOR_ID_PARAM,:PAGE_NO,:PAGE_SIZE,:C2);END;";
            var result = _context.SP_GetAllMeter_Result.FromSqlRaw(sql, p1, p2, p3, p4, p5, p6);//[To-DO]Meter List is returned
            return result;
        }

        public IEnumerable<SP_GetAllVendorLite_Result> GetAllVendorLite()
        {
            OracleParameter p1 = new OracleParameter("C1", OracleDbType.RefCursor, ParameterDirection.Output);
            string sql = "BEGIN SP_GetAllVendorLite(:C1);END;";
            var result = _context.SP_GetAllVendorLite_Result.FromSqlRaw(sql, p1);//[To-DO]Meter List is returned
            return result;
        }

        public IEnumerable<SP_GetUserPrivileges_Result> GetUserPrivileges(int userId)
        {
            OracleParameter p1 = new OracleParameter("USERID_PARAM", OracleDbType.Int32,userId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("C1", OracleDbType.RefCursor, ParameterDirection.Output);
          
            string sql = "BEGIN SP_GETUSERPRIVILEGE(:USERID_PARAM,:C1);END;";
            var result = _context.SP_GetUserPrivileges_Result.FromSqlRaw(sql, p1,p2);//[To-DO]Meter List is returned
            return result;
        }
    }
}
