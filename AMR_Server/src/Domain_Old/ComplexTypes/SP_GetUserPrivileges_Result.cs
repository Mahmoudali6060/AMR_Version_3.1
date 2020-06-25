using AMR_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMR_Server.Domain.ComplexTypes
{
    public class SP_GetUserPrivileges_Result
    {
        public decimal Page_id { get; set; }
        public decimal? Privilege_id { get; set; }
        public string Privilege_name { get; set; }
        public decimal? Privilege_type { get; set; }
       
    }
}
